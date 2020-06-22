using System;
using RabbitMQ.Client;

namespace Gallery.MessageQueues.RabbitMq
{
    public class RabbitMqPublisher : IPublisher
    {
        private readonly string _connectionString;

        public RabbitMqPublisher(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public void SendMessage<T>(T message, string queuePath) where T : class
        {
            
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_connectionString)
            };
            using (var connection = factory.CreateConnection())
            using (var model = connection.CreateModel())
            {
                var messageBody = Serializer.SerializeToBytes(Serializer.SerializeToJson<T>(message));

                model.BasicPublish(string.Empty, queuePath, body: messageBody);
            }
        }
    }
}