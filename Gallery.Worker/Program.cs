using System;
using System.Threading.Tasks;
using Autofac;
using Gallery.Worker.Interfaces;
using Gallery.Worker.Works;
using Topshelf;

namespace Gallery.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = DIConfig.Configure();
            var exitCode = HostFactory.Run(
                config =>
                {
                    config.Service<WorkerWrapper>(sc =>
                    {
                        sc.ConstructUsing(() => new WorkerWrapper(container.ResolveNamed<IWork>(nameof(UploadImageWork))));
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
