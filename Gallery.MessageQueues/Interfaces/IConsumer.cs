namespace Gallery.MessageQueues
{
    public interface IConsumer
    {
        T GetFirstMessage<T>(string queueName) where T : class;
    }
}