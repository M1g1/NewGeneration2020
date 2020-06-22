using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.Http;
using Gallery.App_Start.Modules;
using Gallery.Autofac.Modules;

namespace Gallery.App_Start
{
    public static class DIConfig
    {
        public static IContainer Configure(HttpConfiguration config)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly);

            containerBuilder.RegisterModule<DALModule>();

            containerBuilder.RegisterModule<FileStorageModule>();

            containerBuilder.RegisterModule<ServicesModule>();

            containerBuilder.RegisterModule<MsmqModule>();

            containerBuilder.RegisterModule<RabbitMqModule>();

            containerBuilder.RegisterModule<ControllersModule>();

            var container = containerBuilder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }
    }
}