using Autofac;
using Gallery.Config.Manager;
using Gallery.DAL;
using Gallery.DAL.Models;

namespace Gallery.Autofac.Modules
{
    public class DALModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var connectionString = GalleryConfigurationManager.GetSqlConnectionString();

            containerBuilder.Register(ctx => new GalleryDbContext(connectionString)).AsSelf();

            containerBuilder.RegisterType<UsersRepository>().As<IUserRepository>();

            containerBuilder.RegisterType<MediaRepository>().As<IMediaRepository>();
        }
    }
}