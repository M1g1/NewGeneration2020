using Autofac;
using Gallery.MessageQueues;
using Gallery.MessageQueues.MSMQ;

namespace Gallery.Autofac.Modules
{
    public class MsmqModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var msmqInitializer = new MsmqInitializer();

            msmqInitializer.CreateIfNotExist(Parser.ParseMsmqPaths());

            containerBuilder.Register(pub => new MSMQPublisher()).AsSelf().As<IPublisher>();

            containerBuilder.Register(cons => new MSMQConsumer()).AsSelf().As<IConsumer>();
        }
    }
}