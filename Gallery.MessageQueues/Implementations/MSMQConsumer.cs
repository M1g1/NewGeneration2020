using System;
using System.Messaging;

namespace Gallery.MessageQueues
{
    public class MSMQConsumer : IConsumer
    {
        private readonly MessageQueue _messageQueue;

        public MSMQConsumer(MessageQueue messageQueue)
        {
            _messageQueue = messageQueue ?? throw new ArgumentNullException(nameof(messageQueue));
        }

        public Message ReadFirstMessage()
        { 
            return _messageQueue.Receive();
        }
    }
}