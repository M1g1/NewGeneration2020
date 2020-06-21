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
            var cs = GalleryConfigurationManager.GetRabbitMqConnectionString();

            containerBuilder.Register(p => new RabbitMqParser()).AsSelf().As<IQueueParser>();

            containerBuilder.Register(init => new RabbitMqInitializer(cs)).AsSelf().As<IQueueInitialize>();

            var rabbitMqInitializer = new RabbitMqInitializer(cs);

            var rabbitmqParser = new RabbitMqParser();

            rabbitMqInitializer.CreateIfNotExist(rabbitmqParser.ParseQueuePaths());

            containerBuilder.Register(pub => new RabbitMqPublisher(cs)).AsSelf().As<IPublisher>();

            containerBuilder.Register(cons => new RabbitMqConsumer(cs)).AsSelf().As<IConsumer>();
        }
    }
}