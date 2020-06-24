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

            /*If you need to use MSMQ, uncommit this and commit out another MQ
             containerBuilder.RegisterModule<MsmqModule>();
             */

            /*If you need to use RabbitMq, uncommit this and commit out another MQ
            containerBuilder.RegisterModule<RabbitMqModule>();
            */

            containerBuilder.RegisterModule<AzureMqModule>();

            containerBuilder.RegisterModule<ControllersModule>();

            var container = containerBuilder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }
    }
}