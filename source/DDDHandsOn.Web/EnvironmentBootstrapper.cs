using Agatha.Common;
using Agatha.ServiceLayer;
using Agatha.StructureMap;
using DDDHandsOn.Core;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System;
using System.IO;

namespace DDDHandsOn.Web
{
    public class EnvironmentBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            var bootstrapper = new Bootstrapper(directory);
            var ioc = bootstrapper.Boot();

            var assembly = GetType().Assembly;
            var configurator = new ServiceLayerAndClientConfiguration(assembly, assembly, new Container(ioc));
            configurator.Initialize();

            container.Register<IRequestDispatcherFactory>(ioc.GetInstance<IRequestDispatcherFactory>());
        }
    }
}