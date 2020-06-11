using Autofac;
using Gallery.DAL.Models;
using Gallery.DAL;
using Gallery.Config.Manager;


namespace Gallery.App_Start.Modules
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