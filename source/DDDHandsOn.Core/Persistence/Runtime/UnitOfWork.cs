using DDDHandsOn.Core.DomainModel;
using DDDHandsOn.Core.Persistence.ComponentModel;
using DDDHandsOn.Core.Security.ComponentModel;
using System;
using System.Collections.Generic;

namespace DDDHandsOn.Core.Persistence.Runtime
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly List<IAggregateRoot> _aggregates;
        private readonly IRepository _repository;

        public UnitOfWork(IRepository repository)
        {
            _aggregates = new List<IAggregateRoot>();
            _repository = repository;
        }

        public TAggregate Get<TAggregate>(String id) where TAggregate : IAggregateRoot
        {
            var aggregate = _repository.Get<TAggregate>(id);
            if (aggregate != null)
                Add(aggregate);

            return aggregate;
        }

        public void Add<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot
        {
            _aggregates.Add(aggregate);
        }

        public void Commit(Guid correlationId, IOperationContext context)
        {
            var currentUser = context.GetCurrentUser();
            var now = DateTime.Now;

            Func<IDictionary<String, Object>> headersFactory = () => new Dictionary<String, Object> 
                {
                    { HeaderKeys.Who, currentUser.Name },
                    { HeaderKeys.When, now.ToString("yyyyMMddHHmmsssmmm") },
                    { HeaderKeys.Sequence, now.Ticks },
                    { HeaderKeys.CorrelationId, correlationId.ToString() }
                };

            _aggregates.ForEach(a => _repository.Save(a, headersFactory));
        }

        public void Dispose()
        {
            // TODO: implementare dispose UoW
        }
    }
}
