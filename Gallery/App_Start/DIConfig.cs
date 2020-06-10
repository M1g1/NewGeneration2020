﻿using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.Http;
using Gallery.App_Start.Modules;

namespace Gallery.App_Start
{
    public static class DIConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly);

            containerBuilder.RegisterModule<ControllersModule>();

            containerBuilder.RegisterModule<DALModule>();

            containerBuilder.RegisterModule<FileStorageModule>();

            containerBuilder.RegisterModule<ServicesModule>();

            containerBuilder.RegisterModule<MessageQueuesModule>();

            var container = containerBuilder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}