using DDDHandsOn.Core.DomainModel.ComponentModel;
using System;
using System.Collections.Generic;

namespace DDDHandsOn.Core.DomainModel.Runtime
{
    class DefaultDomainEventsRouter : IDomainEventsRouter
    {
        //private readonly Boolean _throwIfNotHandled;
        private readonly IDictionary<Type, Action<DomainEvent>> _handlers;

        internal DefaultDomainEventsRouter(IDictionary<Type, Action<DomainEvent>> handlers) 
            //: this(true)
        {
            _handlers = handlers;
        }

        //protected DefaultDomainEventsRouter(Boolean throwIfNotHanlded)
        //{
        //    _throwIfNotHandled = throwIfNotHanlded;
        //    _handlers = new Dictionary<Type, Action<DomainEvent>>();
        //}

        public void Dispatch<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent
        {
            var domainEventType = domainEvent.GetType();

            // TODO: check if handler has been registered and throw exception if _throwIfNotHandled is true
            var handler = _handlers[domainEventType];
            handler(domainEvent);
        }
    }
}
