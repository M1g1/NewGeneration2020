using System;
using System.Messaging;

namespace Gallery.MSMQ
{
    public class MSMQPublisher : IPublisher
    {
        private readonly MessageQueue _messageQueue;

        public MSMQPublisher(MessageQueue messageQueue)
        {
            _messageQueue = messageQueue ?? throw new ArgumentNullException(nameof(messageQueue));
        }

        public void SendMessage(object message, string label)
        {
            _messageQueue.Send(message, label);
        }
    }
}
