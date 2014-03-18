using DDDHandsOn.Core.Dispatcher.ComponentModel;
using MassTransit;
using StructureMap;
using System;
using log4net;
using log4net.Config;
using System.IO;

namespace DDDHandsOn.Core
{
    public class Bootstrapper
    {
        private static Object sync = new Object();

        public IContainer Container { get; private set; }
        public String Directory { get; private set; }

        public Bootstrapper(String directory)
        {
            Directory = directory;
        }

        public IContainer Boot()
        {
            lock (sync)
            {
                if (Container == null)
                {
                    ConfigureLogIfAvalialble();

                    Container = new Container();
                    Container.Configure(c =>
                            {
                                c.Scan(s =>
                                            {
                                                s.AssembliesFromPath(Directory, a => a.FullName.Contains("DDDHandsOn"));
                                                s.LookForRegistries();
                                            });

                                c.For<IContainer>().Use(Container);
                                c.For<IServiceProvider>().Use(new StructureMapServiceProviderWrapper(Container));
                                c.For<Bootstrapper>().Use(this);
                            });

                    var busFactory = Container.GetInstance<IServiceBusFactory>();
                    var bus = busFactory.Create();

                    Container.Configure(c => c.For<IServiceBus>().Singleton().Use(bus));
                }

                return Container;
            }
        }

        private void ConfigureLogIfAvalialble()
        {
            var config = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config.xml");
            if (!File.Exists(config)) return;

            XmlConfigurator.Configure(new FileInfo(config));
        }

        private class StructureMapServiceProviderWrapper : IServiceProvider
        {
            private readonly IContainer _container;

            public StructureMapServiceProviderWrapper(IContainer container)
            {
                _container = container;
            }

            public Object GetService(Type serviceType)
            {
                return _container.GetInstance(serviceType);
            }
        }
    }
}
