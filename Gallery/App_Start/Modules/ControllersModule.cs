using Autofac;
using Gallery.Controllers;
using Gallery.Manager;
using Gallery.MessageQueues.MSMQ;
using Gallery.Service;

namespace Gallery.App_Start.Modules
{
    public class ControllersModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AuthenticationManager>().As<IAuthentication>();
            containerBuilder.Register(c =>
                new HomeController(
                    c.Resolve<IImageService>(),
                    c.Resolve<IHashService>(),
                    c.Resolve<IUsersService>(),
                    c.Resolve<MSMQPublisher>()))
                .InstancePerRequest();

            containerBuilder.Register(c =>
                new AccountController(
                    c.Resolve<IUsersService>(),
                    c.Resolve<IAuthentication>()))
            .InstancePerRequest();
        }
    }
}