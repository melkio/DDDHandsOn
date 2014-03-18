using DDDHandsOn.Core.Dispatcher.ComponentModel;
using DDDHandsOn.Core.DomainModel;
using MassTransit;
using NEventStore;
using NEventStore.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDHandsOn.Core.Dispatcher
{
    public class MassTransitDispatchCommits : IDispatchCommits
    {
        private readonly IServiceBus _bus;

        public MassTransitDispatchCommits(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Dispatch(ICommit commit)
        {
            var aggregateId = commit.StreamId;

            commit.Events
                .ToList()
                .ForEach(e =>
                {
                    var domainEvent = e.Body.As<DomainEvent>();

                    domainEvent.AggregateId = aggregateId;
                    domainEvent.InitializeMandatoryHeaders(commit.Headers);
                    domainEvent.Headers.CopyFrom(commit.Headers);
                    domainEvent.Headers.CopyFrom(e.Headers);

                    _bus.Publish(domainEvent, domainEvent.GetType());
                });
        }

        public void Dispose()
        {
            //TODO: implementare dispose del bus in MassTransitDispatchCommits
        }
    }
}

