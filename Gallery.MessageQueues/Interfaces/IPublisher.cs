using System;

namespace Gallery.MessageQueues
{
    public interface IPublisher
    {
        void SendMessage<T>(T message, string queuePath) where T : class;
    }
}