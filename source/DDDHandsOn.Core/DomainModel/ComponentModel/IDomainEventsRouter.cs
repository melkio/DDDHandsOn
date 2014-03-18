using System;

namespace DDDHandsOn.Core.DomainModel.ComponentModel
{
    public interface IDomainEventsRouter
    {
        void Dispatch<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;
    }
}
