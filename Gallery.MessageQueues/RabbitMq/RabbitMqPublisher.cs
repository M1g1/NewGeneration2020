namespace Gallery.MessageQueues.RabbitMq
{
    public class RabbitMqPublisher : IPublisher
    {
        public void SendMessage<T>(T message, string queuePath) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}