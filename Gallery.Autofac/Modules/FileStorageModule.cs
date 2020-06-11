using Autofac;
using FileStorageProvider.Interfaces;
using FileStorageProvider.Providers;
using System.IO.Abstractions;

namespace Gallery.Autofac.Modules
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