using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using Autofac;
using Gallery.Worker.Interfaces;
using Gallery.Worker.Works;

namespace Gallery.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = DIConfig.Configure();
            //if (!Environment.UserInteractive)
            {
                // running as service
                var service = new GalleryWorkerService( new WorkerWrapper(container.ResolveNamed<IWork>(nameof(UploadImageWork))));
                ServiceBase.Run(service);
            }

        }
    }
}
