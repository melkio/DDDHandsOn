using DDDHandsOn.Core.Command;
using System;

namespace DDDHandsOn.Core.Security.ComponentModel
{
    public abstract class Identity
    {
        public static Identity Guest
        {
            get { return new GuestIdentity(); }
        }

        public abstract String Name { get; }
        public abstract String[] Roles { get; }

        public abstract Boolean CanExecute<TRequest>() where TRequest : BaseRequest;

        private class GuestIdentity : Identity
        {
            public override String Name
            {
                get { return "guest"; }
            }

            public override String[] Roles
            {
                get { return new String[0]; }
            }

            public override Boolean CanExecute<TRequest>()
            {
                return false;
            }
        }
    }
}
