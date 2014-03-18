using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace DDDHandsOn.Core.Security.Extensions.Service
{
    public class ImpersonationServiceBehavior : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var dispatchers = serviceHostBase.ChannelDispatchers;
            if (dispatchers == null) return;

            dispatchers
                .Cast<ChannelDispatcher>()
                .SelectMany(d => d.Endpoints)
                .ToList()
                .ForEach(e => e.DispatchRuntime.MessageInspectors.Add(new ServiceImpersonationHeaderMessageInspector()));

        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
