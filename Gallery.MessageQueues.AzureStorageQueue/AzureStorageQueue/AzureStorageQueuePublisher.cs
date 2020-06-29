using System;
using Azure.Storage.Queues;

namespace Gallery.MessageQueues.AzureStorageQueue
{
    public class AzureStorageQueuePublisher : IPublisher
    {
        private readonly string _connectionString;

        public AzureStorageQueuePublisher(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void SendMessage<T>(T message, string queueName) where T : class
        {
            var queueServiceClient = new QueueServiceClient(_connectionString);

            var queueClient = queueServiceClient.GetQueueClient(queueName);

            var messageJson = Serializer.SerializeToJson<T>(message);

            queueClient.SendMessage(messageJson);
        }
    }
}