using DDDHandsOn.Core.DomainModel.ComponentModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DDDHandsOn.Core.DomainModel
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly IDomainEventsRouter _router;
        private readonly IList<DomainEvent> _uncommittedEvents;

        public String Id { get; protected set; }
        public Int32 Version { get; protected set; }

        protected AggregateRoot(IDomainEventsRouterBuilder domainEventsRouterFactory)
        {
            if (domainEventsRouterFactory == null)
                throw new ArgumentNullException("domainEventsRouterFactory");

            Id = Guid.NewGuid().ToString();
            _router = domainEventsRouterFactory.BuildFor(this);
            _uncommittedEvents = new List<DomainEvent>();
        }

        protected void RaiseEvent<TDomainEvent>()
            where TDomainEvent : DomainEvent, new()
        {
            RaiseEvent<TDomainEvent>(e => { });
        }

        protected void RaiseEvent<TDomainEvent>(Action<TDomainEvent> configurator)
            where TDomainEvent : DomainEvent, new()
        {
            RaiseEvent(configurator, () => new Dictionary<String, Object>());
        }

        protected void RaiseEvent<TDomainEvent>(Action<TDomainEvent> configurator, Func<IDictionary<String, Object>> headersFactory)
            where TDomainEvent : DomainEvent, new()
        {
            var domainEvent = new TDomainEvent();
            configurator(domainEvent);
            domainEvent.AggregateId = Id;
            
            this.As<IAggregateRoot>().ApplyEvent(domainEvent);
            
            var headers = headersFactory();
            domainEvent.Headers.CopyFrom(headers);

            _uncommittedEvents.Add(domainEvent);
        }

        void IAggregateRoot.ApplyEvent<TDomainEvent>(TDomainEvent domainEvent)
        {
            _router.Dispatch(domainEvent);
            Version++;
        }

        IEnumerable<DomainEvent> IAggregateRoot.GetUncommittedEvents()
        {
            return new ReadOnlyCollection<DomainEvent>(_uncommittedEvents);
        }

        void IAggregateRoot.ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }
    }
}
