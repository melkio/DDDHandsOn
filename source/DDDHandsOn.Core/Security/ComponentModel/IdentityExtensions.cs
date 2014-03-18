using DDDHandsOn.Core.Command;
using DDDHandsOn.Core.Security.ComponentModel;
using System;
using System.Security;

namespace DDDHandsOn.Core.Security.ComponentModel
{
    internal static class IdentityExtensions
    {
        public static void ThrowSecurityExceptionIfCantExecute<TRequest>(this Identity identity)
            where TRequest : BaseRequest
        {
            var canExecute = identity.CanExecute<TRequest>();

            if (!canExecute)
            {
                var message = String.Format("{0} can't execute {1} operation",
                        identity.Name,
                        typeof(TRequest).FullName);
                throw new SecurityException(message);
            }
        }
    }
}
