using System.IO.Abstractions;
using Autofac;
using FileStorageProvider.Interfaces;
using FileStorageProvider.Providers;

namespace Gallery.App_Start.Modules
{
    public class FileStorageModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<FileSystem>().As<IFileSystem>();

            containerBuilder.RegisterType<MediaStorageProvider>().As<IFileStorage>();
        }
    }
}