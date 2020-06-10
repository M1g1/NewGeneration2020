using Autofac;
using Gallery.Manager;

namespace Gallery.App_Start.Modules
{
    public class ControllersModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AuthenticationManager>().As<IAuthentication>();
        }
    }
}