using DDDHandsOn.Core.DomainModel;
using DDDHandsOn.Core.Security.ComponentModel;
using System;

namespace DDDHandsOn.Core.Persistence.ComponentModel
{
    public interface IUnitOfWork : IDisposable
    {
        TAggregateRoot Get<TAggregateRoot>(String id) where TAggregateRoot : IAggregateRoot;
        void Add<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : IAggregateRoot;
        void Commit(Guid correlationId, IOperationContext context);
    }
}
