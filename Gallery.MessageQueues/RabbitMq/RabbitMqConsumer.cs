namespace Gallery.MessageQueues.RabbitMq
{
    public class RabbitMqConsumer : IConsumer
    {
        public T GetFirstMessage<T>(string messageQueuePath) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}