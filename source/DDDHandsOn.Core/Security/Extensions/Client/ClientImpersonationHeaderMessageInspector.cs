using System;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace DDDHandsOn.Core.Security.Extensions.Client
{
    public class ClientImpersonationHeaderMessageInspector : IClientMessageInspector
    {
        public Object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var identity = Thread.CurrentPrincipal.Identity;
            
            if (identity.IsAuthenticated)
            { 
                var header = MessageHeader.CreateHeader
                    (
                        name: ImpersonationHeader.Name,
                        ns: ImpersonationHeader.Namespace,
                        value: identity.Name
                    );
                request.Headers.Add(header);
            }

            return null;
        }
        
        public void AfterReceiveReply(ref Message reply, Object correlationState)
        {
        }
    }
}
