using Autofac;
using Gallery.Config.Manager;
using Gallery.MessageQueues;

namespace Gallery.App_Start.Modules
{
    public class MessageQueuesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var messageQueuingPath = GalleryConfigurationManager.GetMessageQueuingPath();

            containerBuilder.Register(pub => new MSMQPublisher(messageQueuingPath)).As<IPublisher>();

            containerBuilder.Register(cons => new MSMQConsumer(messageQueuingPath)).As<IConsumer>();
        }
    }
}