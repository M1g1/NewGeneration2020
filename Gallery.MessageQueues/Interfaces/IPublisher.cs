using System;

namespace Gallery.MessageQueues
{
    public interface IPublisher
    {
        void SendMessage(object message, string label);
        void SetFormat(Type[] types);
    }
}