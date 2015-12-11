using DDDHandsOn.Core.DomainModel;
using DDDHandsOn.Core.Persistence.ComponentModel;
using System;
using System.Reflection;

namespace DDDHandsOn.Core.Persistence.Runtime
{
    class DefaultAggregateRootBuilder : IAggregateRootBuilder
    {
        public TAggregateRoot Build<TAggregateRoot>(String id) 
            where TAggregateRoot : IAggregateRoot
        {
            var aggregate = typeof(TAggregateRoot)
                .GetConstructor(new Type[0])
                .Invoke(new Object[0]);

            var property = typeof(TAggregateRoot)
                .GetProperty("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty);
            property
                .SetValue(aggregate, id);

            return (TAggregateRoot)aggregate;
        }
    }
}
