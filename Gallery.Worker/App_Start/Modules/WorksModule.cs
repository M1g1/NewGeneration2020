﻿using Autofac;
using Gallery.MessageQueues;
using Gallery.Service;
using Gallery.Worker;
using Gallery.Worker.Interfaces;
using Gallery.Worker.Works;

namespace Gallery.App_Start.Modules
{
    public class WorksModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register<UploadImageWork>(c=>
                new UploadImageWork(
                    c.Resolve<IConsumer>(),
                    c.Resolve<IImageService>()
                ))
                .AsSelf()
                .As<IWork>();

            containerBuilder.Register(c=> new WorkerWrapper(c.Resolve<UploadImageWork>())).AsSelf();
        }
    }
}