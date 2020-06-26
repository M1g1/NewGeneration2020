using System;
using System.Threading;
using RabbitMQ.Client;

namespace Gallery.MessageQueues.RabbitMq
{
    public class RabbitMqConsumer : IConsumer
    {
        private readonly string _connectionString;
        private readonly TimeSpan _delayReceiveMsg = TimeSpan.FromSeconds(3);

        public RabbitMqConsumer(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public T GetFirstMessage<T>(string messageQueuePath) where T : class
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_connectionString)
            };
            T message;
            using (var connection = factory.CreateConnection())
            using (var model = connection.CreateModel())
            {
                while (true)
                {
                    var msgCount = model.MessageCount(messageQueuePath);
                    if (msgCount > 0)
                    {
                        var getResult = model.BasicGet(messageQueuePath, true);
                        var body = getResult.Body.ToArray();
                        message = Deserializer.DeserializeToObject<T>(Deserializer.DeserializeToString(obj: body));
                        break;
                    }
                    Thread.Sleep(_delayReceiveMsg);
                }
            }
            return message;
        }
    }
}