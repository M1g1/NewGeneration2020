namespace Gallery.MessageQueues.AzureStorageQueue
{
    public class AzureStorageQueuePublisher : IPublisher
    {
        public void SendMessage<T>(T message, string queuePath) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}