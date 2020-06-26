using System;
using RabbitMQ.Client;

namespace Gallery.MessageQueues.RabbitMq
{
    public class RabbitMqInitializer : IQueueInitialize
    {
        private readonly string _connectionString;

        public RabbitMqInitializer(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void CreateIfNotExist(string[] names)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_connectionString)
            };
            using (var connection = factory.CreateConnection())
            using (var model = connection.CreateModel())
            {
                foreach (var name in names)
                {
                    model.QueueDeclare(
                        queue: name,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                }
            }
        }
    }
}