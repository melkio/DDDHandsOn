using DDDHandsOn.Core.DomainModel;
using DDDHandsOn.Core.DomainModel.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DDDHandsOn.Core.Persistence.ComponentModel
{
    public interface IRepository
    {
        TAggregateRoot Get<TAggregateRoot>(String id, Int32 version) 
            where TAggregateRoot : IAggregateRoot;
        IEnumerable<TAggregateRoot> Get<TAggregateRoot, TSnapshot>(Expression<Func<TSnapshot, Boolean>> predicate) 
            where TAggregateRoot : IAggregateRoot, IHaveSnapshotOfType<TSnapshot>
            where TSnapshot : IAggregateSnapshot;
        void Save<TAggregateRoot>(TAggregateRoot aggregate, Func<IDictionary<String, Object>> headersFactory) where TAggregateRoot : IAggregateRoot;
    }
}
