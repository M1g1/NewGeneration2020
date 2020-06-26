namespace Gallery.MessageQueues
{
    public interface IConsumer
    {
        /// <summary>
        /// The method expects messages if they are not in the queue,
        /// and receives the first message from the queue, if any.
        /// </summary>
        /// <typeparam name="T">Return message type.</typeparam>
        /// <param name="queueName">The name of the queue.</param>
        /// <returns>
        /// Returns first message from queue deserialized to type <typeparamref name="T"/>.
        /// </returns>
        T GetFirstMessage<T>(string queueName) where T : class;
    }
}