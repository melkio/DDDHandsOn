using MassTransit;
using System;

namespace DDDHandsOn.Core.Host.Service
{
    class ServiceBusService 
    {
        private IServiceBus _bus;

        public void Start()
        {
            var bootstrapper = new Bootstrapper(AppDomain.CurrentDomain.BaseDirectory);
            var container = bootstrapper.Boot();

            _bus = container.GetInstance<IServiceBus>();
        }

        public void Stop()
        {
            _bus.Dispose();
        }
    }
}
