﻿using Autofac;
using Gallery.App_Start.Modules;
using Gallery.Autofac.Modules;

namespace Gallery.Worker
{
    public class DIConfig
    {
        public static IContainer Configure()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<DALModule>();

            containerBuilder.RegisterModule<FileStorageModule>();

            containerBuilder.RegisterModule<ServicesModule>();

            /*If you need to use MSMQ, uncommit this and commit out another MQ
            containerBuilder.RegisterModule<MsmqModule>();
            */

            containerBuilder.RegisterModule<RabbitMqModule>();

            containerBuilder.RegisterModule<WorksModule>();

            return containerBuilder.Build();
        }
    }
}