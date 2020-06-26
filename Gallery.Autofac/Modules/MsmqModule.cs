using Autofac;
using Gallery.MessageQueues;
using Gallery.MessageQueues.MSMQ;

namespace Gallery.Autofac.Modules
{
    public class MsmqModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            
            containerBuilder.Register(p => new MsmqParser()).AsSelf().As<IQueueParser>();

            containerBuilder.Register(init => new MsmqInitializer()).AsSelf().As<IQueueInitialize>();

            var msmqInitializer = new MsmqInitializer();

            var msmqParser = new MsmqParser();

            msmqInitializer.CreateIfNotExist(msmqParser.ParseQueueNames());

            containerBuilder.Register(pub => new MSMQPublisher()).AsSelf().As<IPublisher>();

            containerBuilder.Register(cons => new MSMQConsumer()).AsSelf().As<IConsumer>();
        }
    }
}