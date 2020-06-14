using Autofac;
using Gallery.Worker.Interfaces;
using Gallery.Worker.Works;

namespace Gallery.App_Start.Modules
{
    public class WorksModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UploadImageWork>().As<IWork>().Named<IWork>(nameof(UploadImageWork));
        }
    }
}