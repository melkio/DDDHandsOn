using System;
using Topshelf;

namespace DDDHandsOn.Core.Host.Service
{
    public class DefaultHost
    {
        public static void Run(String serviceName, String description=null)
        {
            if (String.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentNullException("Impossibile avviare un servizio senza specificarne il nome");

            if (String.IsNullOrWhiteSpace(description))
                description = serviceName;

            HostFactory.Run(c =>
            {
                c.SetServiceName(serviceName);
                c.SetDisplayName(serviceName);
                c.SetDescription(description);

                c.RunAsLocalSystem();
                c.StartAutomatically();

                c.Service<ServiceBusService>(service =>
                {
                    service.ConstructUsing(builder => new ServiceBusService());
                    service.WhenStarted(x => x.Start());
                    service.WhenStopped(x => x.Stop());
                });
            });
        }
    }
}
