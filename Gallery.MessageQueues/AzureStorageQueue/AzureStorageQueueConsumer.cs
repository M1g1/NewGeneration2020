namespace Gallery.MessageQueues.AzureStorageQueue
{
    public class AzureStorageQueueConsumer : IConsumer
    {
        public T GetFirstMessage<T>(string messageQueuePath) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}