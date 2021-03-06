﻿using System;
using System.Threading.Tasks;
using Autofac;
using Gallery.MessageQueues;
using Topshelf;
using Topshelf.Autofac;

namespace Gallery.Worker
{
    internal class Startup
    {
        static async Task Main(string[] args)
        {
            var container = DIConfig.Configure();

            var parsedDictionary = Parser.ParseQueueNames();

            var queueInit = container.Resolve<IQueueInitialize>();

            foreach (var kvp in parsedDictionary)
            {
                queueInit.CreateIfNotExist(kvp.Value);
            }

            var exitCode = HostFactory.Run(
                config =>
                {
                    config.UseAutofacContainer(container);
                    config.Service<WorkerWrapper>(sc =>
                    {
                        sc.ConstructUsingAutofacContainer();
                        // the start and stop methods for the service
                        sc.WhenStarted(async s => await s.StartAsync());
                        sc.WhenStopped(s => s.Stop());
                    });

                    config.RunAsLocalSystem();

                    config.SetDescription("This service for Gallery Worker.");
                    config.SetDisplayName("Worker Wrapper Service");
                    config.SetServiceName("WorkerWrapperService");
                }
            );

            var exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;

        }
    }
}
