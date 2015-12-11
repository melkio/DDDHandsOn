using System;
using System.Collections.Generic;

namespace DDDHandsOn.Core.DomainModel
{
    public interface IAggregateRoot
    {
        String Id { get; }
        Int32 Version { get; }

        void ApplyEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;
        IEnumerable<DomainEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }

    public interface IAggregateSnapshot
    {
        String Id { get; }
        Int32 Version { get; }
    }
}
