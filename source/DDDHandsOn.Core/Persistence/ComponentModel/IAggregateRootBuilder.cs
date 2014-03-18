using DDDHandsOn.Core.DomainModel;
using System;

namespace DDDHandsOn.Core.Persistence.ComponentModel
{
    public interface IAggregateRootBuilder
    {
        TAggregateRoot Build<TAggregateRoot>(String id) where TAggregateRoot : IAggregateRoot;
    }
}
