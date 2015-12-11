using DDDHandsOn.Core.DomainModel;
using DDDHandsOn.Core.Persistence;
using System;

namespace DDDHandsOn.Web.Domain
{
    [StoreBucket("demo")]
    public class Echo : AggregateRootMappedByConventions
    {
        String _value;
        
        public void Execute(String value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            RaiseEvent<EchoExecuted>(e => e.Value = value);
        }

        void Apply(EchoExecuted domainEvent)
        {
            _value = domainEvent.Value;
        }
    }

    public class EchoExecuted : DomainEvent
    {
        public String Value { get; set; }
    }
}