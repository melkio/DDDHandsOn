using DDDHandsOn.Core.Dispatcher.ComponentModel;
using MassTransit;
using MassTransit.Log4NetIntegration;
using StructureMap;
using System.Configuration;

namespace DDDHandsOn.Core.Dispatcher.Runtime
{
    class MsmqServiceBusFactory : IServiceBusFactory
    {
        private readonly IContainer _container;

        public MsmqServiceBusFactory(IContainer container)
        {
            _container = container;
        }

        public IServiceBus Create()
        {
            var queue = ConfigurationManager.AppSettings["bus"];
            var subscriptionService = ConfigurationManager.AppSettings["subscriptionService"];

            Bus.Initialize(c =>
            {
                c.ReceiveFrom(queue);
                c.UseMsmq(x => x.UseSubscriptionService(subscriptionService));
                c.UseLog4Net();
                c.SetConcurrentConsumerLimit(1);
                c.UseJsonSerializer();

                c.Subscribe(s => s.LoadFrom(_container));
            });

            return Bus.Instance;
        }
    }
}
