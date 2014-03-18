using DDDHandsOn.Core.DomainModel;
using System;
using System.Collections.Generic;

namespace DDDHandsOn.Core.Persistence.ComponentModel
{
    public static class RepositoryExtensions
    {
        public static TAggregateRoot Get<TAggregateRoot>(this IRepository repository, String id) where TAggregateRoot : IAggregateRoot
        {
            return repository.Get<TAggregateRoot>(id, Int32.MaxValue);
        }

        public static void Save<TAggregateRoot>(this IRepository repository, TAggregateRoot aggregate) where TAggregateRoot : IAggregateRoot
        {
            repository.Save(aggregate, () => new Dictionary<String, Object>());
        }
    }
}
