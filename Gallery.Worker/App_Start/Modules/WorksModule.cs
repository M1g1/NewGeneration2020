using Autofac;
using Gallery.Worker;
using Gallery.Worker.Interfaces;
using Gallery.Worker.Works;

namespace Gallery.App_Start.Modules
{
    public class WorksModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UploadImageWork>().AsSelf().As<IWork>();

            containerBuilder.Register(c=> new WorkerWrapper(c.Resolve<UploadImageWork>())).AsSelf();
        }
    }
}