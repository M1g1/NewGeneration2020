using Autofac;
using Gallery.MessageQueues;
using Gallery.MessageQueues.MSMQ;

namespace Gallery.Autofac.Modules
{
    public class MsmqModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {

            containerBuilder.Register(init => new MsmqInitializer()).AsSelf().As<IQueueInitialize>();

            containerBuilder.Register(pub => new MSMQPublisher()).AsSelf().As<IPublisher>();

            containerBuilder.Register(cons => new MSMQConsumer()).AsSelf().As<IConsumer>();
        }
    }
}