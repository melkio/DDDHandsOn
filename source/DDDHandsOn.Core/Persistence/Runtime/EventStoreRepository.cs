using DDDHandsOn.Core.DomainModel;
using DDDHandsOn.Core.DomainModel.ComponentModel;
using DDDHandsOn.Core.Persistence.ComponentModel;
using DDDHandsOn.Core.Security.ComponentModel;
using NEventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DDDHandsOn.Core.Persistence.Runtime
{
    class EventStoreRepository : IRepository, IDisposable
    {
        private readonly IStoreEvents _store;
        private readonly IAggregateRootBuilder _factory;

        public EventStoreRepository(IStoreEvents store, IAggregateRootBuilder factory)
        {
            _store = store;
            _factory = factory;
        }

        public TAggregateRoot Get<TAggregateRoot>(String id, Int32 version)
            where TAggregateRoot : IAggregateRoot
        {
            var bucket = typeof(TAggregateRoot)
                .GetAttribute<StoreBucketAttribute>()
                .ComposeFor<TAggregateRoot>();

            var stream = _store.OpenStream(bucket, id, 0, version);
            if (stream.CommittedEvents.Count == 0)
                return default(TAggregateRoot);

            var aggregate = _factory.Build<TAggregateRoot>(id);

            var events = stream.CommittedEvents
                .Select(e => e.Body)
                .Cast<DomainEvent>()
                .ToArray();

            Array.ForEach(events, e => ((IAggregateRoot)aggregate).ApplyEvent(e));
            return aggregate;
        }

        public IEnumerable<TAggregateRoot> Get<TAggregateRoot, TSnapshot>(Expression<Func<TSnapshot, Boolean>> predicate)
            where TAggregateRoot : IAggregateRoot, IHaveSnapshotOfType<TSnapshot>
            where TSnapshot : IAggregateSnapshot
        {
            throw new NotImplementedException();
        }

        public void Save<TAggregateRoot>(TAggregateRoot aggregate, Func<IDictionary<String, Object>> headersFactory) 
            where TAggregateRoot : IAggregateRoot
        {
            var aggregateType = aggregate.GetType();
            var bucket = aggregateType
                .GetAttribute<StoreBucketAttribute>()
                .ComposeFor(aggregateType);

            var stream = _store.OpenStream(bucket, aggregate.Id, 0, Int32.MaxValue);

            var events = aggregate.GetUncommittedEvents()
                .Select(e => new EventMessage { Body = e, Headers = e.Headers.CustomHeaders.ToDictionary(x => x.Key, x=>x.Value)})
                .ToArray();
            Array.ForEach(events, e => stream.Add(e));

            var headers = headersFactory();
            headers[HeaderKeys.AggregateType] = aggregateType.FullName;
            ThrowIfHeadersAreNotConfiguredProperly(headers);
            Array.ForEach(headers.ToArray(), h => stream.UncommittedHeaders.Add(h));

            stream.CommitChanges(Guid.NewGuid());

            var snapshottable = aggregate as IHaveSnapshot;
            if (snapshottable != null)
            {
                var snapshot = new Snapshot
                    (
                        bucketId: bucket, 
                        streamId: aggregate.Id, 
                        streamRevision: stream.StreamRevision, 
                        payload: snapshottable.GetSnapshot()
                    );
                _store.Advanced.AddSnapshot(snapshot);
            }
            
            aggregate.ClearUncommittedEvents();
        }

        public void Dispose()
        {
            _store.Dispose();
        }

        private void ThrowIfHeadersAreNotConfiguredProperly(IDictionary<String, Object> headers)
        {
            var isValid = HeaderKeys.Mandatory
                .Select(k => headers.ContainsKey(k))
                .All(v => v);

            if (!isValid)
                throw new ArgumentException("Commit headers are not configured properly", "headers");
        }
        
    }
}
