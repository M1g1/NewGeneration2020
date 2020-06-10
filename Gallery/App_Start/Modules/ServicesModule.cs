﻿using Autofac;
using Gallery.Service;

namespace Gallery.App_Start.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UsersService>().As<IUsersService>();

            containerBuilder.RegisterType<ImageService>().As<IImageService>();

            containerBuilder.RegisterType<HashService>().As<IHashService>();
        }
    }
}