using DDDHandsOn.Core.DomainModel.Runtime;

namespace DDDHandsOn.Core.DomainModel
{
    public abstract class AggregateRootMappedByConventions : AggregateRoot
    {
        protected AggregateRootMappedByConventions()
            : base(new DomainEventsRouterBuilderByConventions())
        {

        }
    }
}
