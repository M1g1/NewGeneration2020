namespace Gallery.MessageQueues
{
    public interface IPublisher
    {
        void SendMessage<T>(T message, string queueName) where T : class;
    }
}