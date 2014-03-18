using DDDHandsOn.Core.Security.ComponentModel;
using DDDHandsOn.Core.Security.Runtime;
using StructureMap.Configuration.DSL;

namespace DDDHandsOn.Core.Security.Configuration
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<IOperationContext>()
                .Transient()
                .Use<AgathaOperationContext>();
        }
    }
}
