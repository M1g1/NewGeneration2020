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

        public void CreateIfNotExist(string[] paths)
        {
            var queueServiceClient = new QueueServiceClient(_connectionString);
            foreach (var path in paths)
            {
                var queueClient = queueServiceClient.GetQueueClient(path);
                queueClient.CreateIfNotExists();
            }
        }
    }
}