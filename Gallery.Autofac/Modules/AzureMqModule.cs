using Autofac;
using Gallery.Config.Manager;
using Gallery.MessageQueues;
using Gallery.MessageQueues.AzureStorageQueue;

namespace Gallery.Autofac.Modules
{
    public class AzureMqModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var cs = GalleryConfigurationManager.GetAzureMqConnectionString();

            containerBuilder.Register(init => new AzureStorageQueueInitializer(cs)).AsSelf().As<IQueueInitialize>();

            containerBuilder.Register(pub => new AzureStorageQueuePublisher(cs)).AsSelf().As<IPublisher>();

            containerBuilder.Register(cons => new AzureStorageQueueConsumer(cs)).AsSelf().As<IConsumer>();
        }
    }
}