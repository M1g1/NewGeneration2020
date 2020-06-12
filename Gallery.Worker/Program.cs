using System;
using System.Threading.Tasks;
using Autofac;
using Gallery.Worker.Works;


namespace Gallery.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = DIConfig.Configure();

            var wrapper = new WorkerWrapper(container.Resolve<UploadImageWork>());

            await wrapper.StartAsync();

            var input = Console.ReadKey();

            wrapper.Stop();
        }
    }
}
