namespace Gallery.MessageQueues
{
    public interface IConsumer
    {
        T GetFirstMessage<T>(string messageQueuePath) where T : class;
    }
}