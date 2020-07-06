using System;

namespace Gallery.MessageQueues
{
    public interface IConsumer
    {
        /// <summary>
        /// The method expects messages if they are not in the queue,
        /// receives the first message from the queue, if any, and performs some actions on it.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="action">Some action.</param>
        void Consume<T>(string queueName, Action<T> action) where T : class;
    }
}