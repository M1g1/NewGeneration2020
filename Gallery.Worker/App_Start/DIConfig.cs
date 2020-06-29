using Autofac;
using Gallery.App_Start.Modules;
using Gallery.Autofac.Modules;

namespace Gallery.Worker
{
    public class DIConfig
    {
        public static IContainer Configure()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<DALModule>();

            containerBuilder.RegisterModule<FileStorageModule>();

            containerBuilder.RegisterModule<ServicesModule>();

            containerBuilder.RegisterModule<RabbitMqModule>();
           
            containerBuilder.RegisterModule<WorksModule>();

            return containerBuilder.Build();
        }
    }
}