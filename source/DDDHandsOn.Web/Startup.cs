using Agatha.ServiceLayer;
using Agatha.StructureMap;
using DDDHandsOn.Core;
using DDDHandsOn.Web;
using Microsoft.Owin;
using Nancy.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(Startup))]

namespace DDDHandsOn.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new NancyOptions();
            options.Bootstrapper = new EnvironmentBootstrapper();

            app.MapSignalR();
            app.UseNancy(options);

        }
    }
}