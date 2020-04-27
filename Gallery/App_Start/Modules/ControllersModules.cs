using Autofac;
using Autofac.Integration.Mvc;
using Gallery.Service;
using Gallery.DAL.Models;
using Gallery.DAL;
using Gallery.Manager;

namespace Gallery.App_Start.Modules
{
    public class ControllersModules: Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly);

            var connectionString = GalleryConfigurationManager.GetSqlConnectionString();

            containerBuilder.Register(ctx => new UserContext(connectionString)).AsSelf();

            containerBuilder.RegisterType<UsersRepository>().As<IRepository>();

            containerBuilder.RegisterType<UsersService>().As<IUsersService>();

            containerBuilder.RegisterType<AuthenticationManager>().As<IAuthentication>();

            containerBuilder.RegisterType<ImageService>().As<IImageService>();

            containerBuilder.RegisterType<HashService>().As<IHashService>();
        }
    }
}