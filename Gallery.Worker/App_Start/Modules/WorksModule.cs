using Autofac;
using FileStorageProvider.Providers;
using Gallery.DAL;
using Gallery.MessageQueues.MSMQ;
using Gallery.Service;
using Gallery.Worker;
using Gallery.Worker.Interfaces;
using Gallery.Worker.Works;

namespace Gallery.App_Start.Modules
{
    public class WorksModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register<UploadImageWork>(c=>
                new UploadImageWork(
                    c.Resolve<MSMQConsumer>(),
                    c.Resolve<MediaStorageProvider>(),
                    c.Resolve<IImageService>(),
                    c.Resolve<IMediaRepository>(),
                    c.Resolve<MsmqParser>()
                    ))
                .AsSelf()
                .As<IWork>();

            containerBuilder.Register(c=> new WorkerWrapper(c.Resolve<UploadImageWork>())).AsSelf();
        }
    }
}