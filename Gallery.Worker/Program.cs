using System;
using System.Threading.Tasks;
using FileStorageProvider.Providers;
using Gallery.MessageQueues;
using System.IO.Abstractions;
using System.Threading;
using Gallery.Config.Manager;
using Gallery.DAL;
using Gallery.DAL.Models;
using Gallery.Service;


namespace Gallery.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var allStops = new CancellationTokenSource();
            var connectionString = GalleryConfigurationManager.GetSqlConnectionString();
            var messageQueuingPath = GalleryConfigurationManager.GetMessageQueuingPath();
            IWork work = new UploadImageWork(
            new MSMQConsumer(messageQueuingPath),
            new MediaStorageProvider(new FileSystem()),
            new ImageService(
                new MediaStorageProvider(new FileSystem()),
                new MediaRepository(new GalleryDbContext(connectionString)),
                new UsersRepository(new GalleryDbContext(connectionString))),
            new MediaRepository(new GalleryDbContext(connectionString)));

            await Task.Factory.StartNew(work.StartAsync, 
                allStops.Token, 
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current);

           
            allStops.Cancel();
            work.Stop();
        }
    }
}
