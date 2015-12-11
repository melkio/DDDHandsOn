using System;
using System.ServiceModel.Configuration;

namespace DDDHandsOn.Core.Security.Extensions.Service
{
    public class ImpersonationServiceElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ImpersonationServiceBehavior); }
        }

        protected override Object CreateBehavior()
        {
            return new ImpersonationServiceBehavior();
        }
    }
}
