using MassTransit;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DDDHandsOn.Core.DomainModel
{
    public interface IDomainEvent
    {
        String AggregateId { get; }
        DomainEventHeaders Headers { get; }
    }

    public abstract class DomainEvent : IDomainEvent, CorrelatedBy<Guid>
    {
        [BsonIgnore]
        public String AggregateId { get; set; }
        [BsonIgnore]
        public DomainEventHeaders Headers { get; set; }

        protected DomainEvent()
        {
            Headers = new DomainEventHeaders();
        }

        Guid CorrelatedBy<Guid>.CorrelationId
        {
            get { return Headers.CorrelationId; }
        }
    }

}
