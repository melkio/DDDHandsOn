using DDDHandsOn.Core.DomainModel.ComponentModel;
using System;
using System.Linq;
using System.Reflection;

namespace DDDHandsOn.Core.DomainModel.Runtime
{
    class DomainEventsRouterBuilderByConventions : IDomainEventsRouterBuilder
    {
        public IDomainEventsRouter BuildFor(IAggregateRoot aggregate)
        {
            var handlers = aggregate.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "Apply" 
                    && m.GetParameters().Length == 1 && typeof(DomainEvent).IsAssignableFrom(m.GetParameters().Single().ParameterType) 
                    && m.ReturnParameter.ParameterType == typeof(void))
                .ToDictionary
                    (
                        m => m.GetParameters().Single().ParameterType,
                        m => 
                            {
                                Action<DomainEvent> handler = e => m.Invoke(aggregate, new[] { e });
                                return handler;
                            }
                    );

            return new DefaultDomainEventsRouter(handlers);
        }
    }
}
