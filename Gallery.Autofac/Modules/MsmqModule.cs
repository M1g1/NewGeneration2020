using Autofac;
using Gallery.Config.Manager;
using Gallery.MessageQueues;

namespace Gallery.Autofac.Modules
{
    public class MsmqModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var queuePaths = GalleryConfigurationManager.GetMsmqPaths();

            MsmqInitializer.CreateIfNotExist(Parser.ParseQueuePaths());

            containerBuilder.Register(pub => new MSMQPublisher(queuePaths)).AsSelf().As<IPublisher>();

            containerBuilder.Register(cons => new MSMQConsumer()).AsSelf().As<IConsumer>();
        }
    }
}