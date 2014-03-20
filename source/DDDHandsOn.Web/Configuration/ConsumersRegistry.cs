using DDDHandsOn.Web.Denormalizers;
using StructureMap.Configuration.DSL;
using System;

namespace DDDHandsOn.Web.Configuration
{
    public class ConsumersRegistry : Registry
    {
        public ConsumersRegistry()
        {
            For<EchoReadModel>().Use<EchoReadModel>();
        }
    }
}