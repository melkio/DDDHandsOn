using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDHandsOn.Core.DomainModel
{
    public static class DomainEventExtensionMethods
    {
        public static void InitializeMandatoryHeaders(this IDomainEvent domainEvent, IDictionary<String, Object> headers)
        {
            if (!headers.ContainsAllHeaders())
                throw new InvalidOperationException("Isn't possible initialize headers with missing keys");

            var correlationId = headers[HeaderKeys.CorrelationId].ToString();
            var who = headers[HeaderKeys.Who].ToString();
            var when = headers[HeaderKeys.When].ToString();
            var type = headers[HeaderKeys.AggregateType].ToString();

            domainEvent.Headers.CorrelationId = Guid.Parse(correlationId);
            domainEvent.Headers.When = DateTime.ParseExact(when, Constants.DateTimeFormat, null);
            domainEvent.Headers.Who = who;
            domainEvent.Headers.AggregateType = type;
        }
    }
}
