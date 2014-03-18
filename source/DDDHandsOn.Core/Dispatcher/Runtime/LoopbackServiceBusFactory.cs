using DDDHandsOn.Core.Dispatcher.ComponentModel;
using MassTransit;
using MassTransit.Log4NetIntegration;
using StructureMap;

namespace DDDHandsOn.Core.Dispatcher.Runtime
{
    class LoopbackServiceBusFactory : IServiceBusFactory
    {
        private readonly IContainer _container;

        public LoopbackServiceBusFactory(IContainer container)
        {
            _container = container;
        }

        public IServiceBus Create()
        {
            return ServiceBusFactory.New(c =>
                {
                    c.ReceiveFrom("loopback://localhost/queue");
                    c.UseJsonSerializer();
                    c.SetConcurrentConsumerLimit(1);
                    c.UseLog4Net("masstransit.config.xml");

                    c.Subscribe(s => s.LoadFrom(_container));
                });
        }
    }
}
