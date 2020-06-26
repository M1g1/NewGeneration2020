namespace Gallery.MessageQueues
{
    public interface IPublisher
    {
        /// <summary>
        /// Send message to queue.
        /// </summary>
        /// <typeparam name="T">Type of message to send.</typeparam>
        /// <param name="message">Message to send.</param>
        /// <param name="queueName">The name of the queue.</param>
        void SendMessage<T>(T message, string queueName) where T : class;
    }
}