using System;
using Azure.Storage.Queues;

namespace Gallery.MessageQueues.AzureStorageQueue
{
    public class AzureStorageQueueInitializer : IQueueInitialize
    {
        private readonly string _connectionString;

        public AzureStorageQueueInitializer(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void CreateIfNotExist(string queueName)
        {
            var queueServiceClient = new QueueServiceClient(_connectionString);
            var queueClient = queueServiceClient.GetQueueClient(queueName);
            queueClient.CreateIfNotExists();
        }
    }
}