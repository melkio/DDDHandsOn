using System;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace DDDHandsOn.Core.Security.Extensions.Service
{
    public class ServiceImpersonationHeaderMessageInspector : IDispatchMessageInspector
    {
        public void BeforeSendReply(ref Message reply, Object correlationState)
        {
            
        }

        public Object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var headerIndex = request.Headers.FindHeader
                (
                    name: ImpersonationHeader.Name, 
                    ns: ImpersonationHeader.Namespace
                );

            if (headerIndex >= 0)
            {
                var header = request.Headers.GetHeader<String>(headerIndex);
                request.Headers.RemoveAt(headerIndex);

                var identity = new GenericIdentity(header);
                Thread.CurrentPrincipal = new GenericPrincipal(identity, new String[0]);
            }

            return null;
        }
    }
}
