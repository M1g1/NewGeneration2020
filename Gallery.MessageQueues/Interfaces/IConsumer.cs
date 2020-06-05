using System.Messaging;

namespace Gallery.MessageQueues
{
    public interface IConsumer
    {
        Message ReadFirstMessage();
    }
}