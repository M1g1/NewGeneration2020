using System;

namespace Gallery.MessageQueues
{
    public interface IConsumer
    {
        object GetFirstMessageBody();
        void SetFormat(Type[] types);
    }
}