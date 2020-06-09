using System.Threading.Tasks;
using FileStorageProvider.Providers;
using Gallery.MessageQueues;
using System.IO.Abstractions;
using Gallery.DAL;
using Gallery.DAL.Models;
using Gallery.Service;
using Gallery.Worker.Manager;

namespace Gallery.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var connectionString = GalleryWorkerConfigurationManager.GetSqlConnectionString();
            var messageQueuingPath = GalleryWorkerConfigurationManager.GetMessageQueuingPath();
            IWork a = new Work(
            new MSMQConsumer(messageQueuingPath),
            new MediaStorageProvider(new FileSystem()),
            new ImageService(
                new MediaStorageProvider(new FileSystem()),
                new MediaRepository(new GalleryDbContext(connectionString)),
                new UsersRepository(new GalleryDbContext(connectionString))),
            new MediaRepository(new GalleryDbContext(connectionString)));
            int i = 0;
            while (true)
            {
                //Console.WriteLine(i++);
                await a.Upload();
            }


        }
    }
}
