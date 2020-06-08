using System;
using System.Messaging;
using System.Threading.Tasks;
using FileStorageProvider.Providers;
using Gallery.MessageQueues;
using System.IO.Abstractions;
using Gallery.DAL;
using Gallery.DAL.Models;
using Gallery.Service;
using Gallery.Service.Contract;

namespace Gallery.Worker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var messageQueue = new MessageQueue(@".\private$\MQ"))
            {
                if (!MessageQueue.Exists(messageQueue.Path))
                    MessageQueue.Create(messageQueue.Path);
                messageQueue.Formatter = new XmlMessageFormatter(
                    new Type[]
                    {
                        typeof(MessageDto)

                    });
                var con =
                    @"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog = Gallery; Integrated Security = True";
                var mp = new MediaStorageProvider(new FileSystem());
                var msmqConsumer = new MSMQConsumer(messageQueue);
                IWork a = new Work(
                    msmqConsumer,
                    mp,
                    new ImageService(
                        new MediaStorageProvider(new FileSystem()),
                        new MediaRepository(new GalleryDbContext(con)),
                        new UsersRepository(new GalleryDbContext(con))),
                    new MediaRepository(new GalleryDbContext(con)));
                int i = 0;
                while (true)
                {
                    //Console.WriteLine(i++);
                    await a.Upload();
                }
            }
            
        }
    }
}
