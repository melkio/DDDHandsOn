using DDDHandsOn.Core.Dispatcher.ComponentModel;
using MassTransit;
using MassTransit.Log4NetIntegration;
using StructureMap;
using System;
using System.Configuration;
using System.Linq;

namespace DDDHandsOn.Core.Dispatcher.Runtime
{
    class RabbitMqServiceBusFactory : IServiceBusFactory
    {
        private readonly IContainer _container;

        public RabbitMqServiceBusFactory(IContainer container)
        {
            _container = container;
        }

        public IServiceBus Create()
        {
            var queue = ConfigurationManager.AppSettings["bus"];
            var concurrentConsumerLimit = ConfigurationManager.AppSettings.AllKeys.Contains("instances") ?
                Convert.ToInt32(ConfigurationManager.AppSettings["instances"]) : 1;

            Bus.Initialize(c =>
            {
                c.ReceiveFrom(queue);
                c.UseRabbitMqRouting();
                c.UseLog4Net();
                c.SetConcurrentConsumerLimit(concurrentConsumerLimit);
                c.UseJsonSerializer();

                c.Subscribe(s => s.LoadFrom(_container));
            });

            return Bus.Instance;
        }
    }
}
