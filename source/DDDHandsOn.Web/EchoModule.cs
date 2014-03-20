using Agatha.Common;
using DDDHandsOn.Web.Commands;
using Nancy;
using System;

namespace DDDHandsOn.Web
{
    public class EchoModule : NancyModule
    {
        public EchoModule(IRequestDispatcherFactory factory) 
            : base("/echo")
        {
            Get[""] = p => 
            {
                using (var dispatcher = factory.CreateRequestDispatcher())
                {
                    var command = new EchoRequest { Value = "Alessandro" };
                    var response = dispatcher.Get<EchoResponse>(command);
                    return response.Value;
                }
                
            };
        }
    }
}