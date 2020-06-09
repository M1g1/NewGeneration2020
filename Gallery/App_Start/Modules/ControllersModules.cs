using System.IO.Abstractions;
using Autofac;
using FileStorageProvider.Interfaces;
using FileStorageProvider.Providers;
using Gallery.Service;
using Gallery.DAL.Models;
using Gallery.DAL;
using Gallery.Manager;
using Gallery.MessageQueues;

namespace Gallery.App_Start.Modules
{
    public class ControllersModules : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var connectionString = GalleryConfigurationManager.GetSqlConnectionString();

            containerBuilder.Register(ctx => new GalleryDbContext(connectionString)).AsSelf();

            var messageQueuingPath = GalleryConfigurationManager.GetMessageQueuingPath();

            containerBuilder.Register(pub => new MSMQPublisher(messageQueuingPath)).As<IPublisher>();
                
            containerBuilder.Register(cons => new MSMQConsumer(messageQueuingPath)).As<IConsumer>();

            containerBuilder.RegisterType<UsersRepository>().As<IUserRepository>();

            containerBuilder.RegisterType<UsersService>().As<IUsersService>();

            containerBuilder.RegisterType<AuthenticationManager>().As<IAuthentication>();

            containerBuilder.RegisterType<ImageService>().As<IImageService>();

            containerBuilder.RegisterType<FileSystem>().As<IFileSystem>();

            containerBuilder.RegisterType<HashService>().As<IHashService>();

            containerBuilder.RegisterType<MediaStorageProvider>().As<IFileStorage>();

            containerBuilder.RegisterType<MediaRepository>().As<IMediaRepository>();
        }
    }
}