using System;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Gallery.MessageQueues.RabbitMq
{
    public class RabbitMqConsumer : IConsumer
    {
        private readonly string _connectionString;
        private readonly EventWaitHandle _waitHandle = new AutoResetEvent(false);

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
            using (var connection = factory.CreateConnection())
            using (var model = connection.CreateModel())
            {
                T message = default(T);

                var consumer = new EventingBasicConsumer(model);

                consumer.Received += (eventModel, ea) =>
                {
                    var body = ea.Body.ToArray();
                    message = Deserializer.DeserializeToObject<T>(Deserializer.DeserializeToString(obj: body));
                    _waitHandle.Set();
                };
                model.BasicConsume(
                    queue: messageQueuePath,
                    autoAck: true,
                    consumer: consumer);

                _waitHandle.WaitOne();
                return message;
            }
        }
    }
}