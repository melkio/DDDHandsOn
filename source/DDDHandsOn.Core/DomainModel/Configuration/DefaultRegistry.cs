using DDDHandsOn.Core.DomainModel.ComponentModel;
using DDDHandsOn.Core.DomainModel.Runtime;
using StructureMap.Configuration.DSL;
using System;

namespace DDDHandsOn.Core.DomainModel.Configuration
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<IIdentityProvider>()
                .Singleton()
                .Use<IdentityProvider>();
        }
    }
}
