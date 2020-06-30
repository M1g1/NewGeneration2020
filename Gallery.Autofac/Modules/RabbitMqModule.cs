using Autofac;
using Gallery.Config.Manager;
using Gallery.MessageQueues;
using Gallery.MessageQueues.RabbitMq;

namespace Gallery.Autofac.Modules
{
    public class RabbitMqModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var rabbitmqConnectionStringName = "RabbitMqConnection";

            var cs = GalleryConfigurationManager.GetConnectionString(rabbitmqConnectionStringName);

            containerBuilder.Register(init => new RabbitMqInitializer(cs)).AsSelf().As<IQueueInitialize>();

            containerBuilder.Register(pub => new RabbitMqPublisher(cs)).AsSelf().As<IPublisher>();

            containerBuilder.Register(cons => new RabbitMqConsumer(cs)).AsSelf().As<IConsumer>();
        }
    }
}