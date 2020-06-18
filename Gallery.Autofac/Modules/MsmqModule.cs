using Autofac;
using Gallery.Config.Manager;
using Gallery.MessageQueues;

namespace Gallery.Autofac.Modules
{
    public class MsmqModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var messageQueuingPath = GalleryConfigurationManager.GetMessageQueuingPath();

            containerBuilder.Register(pub => new MSMQPublisher(messageQueuingPath)).AsSelf().As<IPublisher>();

            containerBuilder.Register(cons => new MSMQConsumer(messageQueuingPath)).AsSelf().As<IConsumer>();
        }
    }
}