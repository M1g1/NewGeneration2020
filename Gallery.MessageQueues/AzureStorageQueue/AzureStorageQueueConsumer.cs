using System;
using System.Threading;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Gallery.MessageQueues.AzureStorageQueue
{
    public class AzureStorageQueueConsumer : IConsumer
    {
        private readonly string _connectionString;
        private readonly TimeSpan _delayReceiveMsg = TimeSpan.FromSeconds(3);
        private readonly TimeSpan _visibilityDelay = TimeSpan.FromSeconds(1);

        public AzureStorageQueueConsumer(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public T GetFirstMessage<T>(string messageQueuePath) where T : class
        {
            var queueServiceClient = new QueueServiceClient(_connectionString);

            var queueClient = queueServiceClient.GetQueueClient(messageQueuePath);
            var msg = string.Empty;
            while (true)
            {
                QueueProperties queueProp = queueClient.GetProperties();
                var msgCount = queueProp.ApproximateMessagesCount;
                if (msgCount > 0)
                {
                    QueueMessage[] receiveMessages = queueClient.ReceiveMessages(maxMessages: 1, _visibilityDelay);

                    if (receiveMessages.Length < 1)
                        continue;

                    queueClient.DeleteMessage(receiveMessages[0].MessageId, receiveMessages[0].PopReceipt);

                    msg = receiveMessages[0].MessageText;
                    break;
                }
                Thread.Sleep(_delayReceiveMsg);
            }
            return Deserializer.DeserializeToObject<T>(msg);
        }
    }
}