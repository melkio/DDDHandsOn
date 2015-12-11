using DDDHandsOn.Core.Dispatcher.ComponentModel;
using DDDHandsOn.Core.Dispatcher.Runtime;
using NHibernate;
using StructureMap.Configuration.DSL;
using System.Configuration;
using System.Linq;

namespace DDDHandsOn.Core.Dispatcher.Configuration
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            InstanceOf<IServiceBusFactory>()
                .Is.Conditional(e =>
                    {
                        e.If(c => ConfigurationManager.AppSettings.AllKeys.Contains("bus") && ConfigurationManager.AppSettings["bus"].StartsWith("msmq"))
                            .ThenIt.Is.Type<MsmqServiceBusFactory>();
                        e.If(c => ConfigurationManager.AppSettings.AllKeys.Contains("bus") && ConfigurationManager.AppSettings["bus"].StartsWith("rabbitmq"))
                            .ThenIt.Is.Type<RabbitMqServiceBusFactory>();
                        e.TheDefault.Is.Type<LoopbackServiceBusFactory>();
                    });
        }
    }
}
