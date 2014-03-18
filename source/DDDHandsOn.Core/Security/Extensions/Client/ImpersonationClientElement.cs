using System;
using System.ServiceModel.Configuration;

namespace DDDHandsOn.Core.Security.Extensions.Client
{
    public class ImpersonationClientElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ImpersonationClientBehavior); }
        }

        protected override Object CreateBehavior()
        {
            return new ImpersonationClientBehavior();
        }
    }
}
