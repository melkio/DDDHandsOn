using System;

namespace DDDHandsOn.Core.DomainModel.ComponentModel
{
    public interface IDomainEventsRouterBuilder
    {
        IDomainEventsRouter BuildFor(IAggregateRoot aggregate);
    }
}
