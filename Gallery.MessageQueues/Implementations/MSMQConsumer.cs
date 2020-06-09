using System;
using System.Messaging;

namespace Gallery.MessageQueues
{
    public class MSMQConsumer : IConsumer
    {
        private readonly MessageQueue _messageQueue;
        private readonly string _messageQueuePath;

        public MSMQConsumer(string messageQueuePath)
        {
            _messageQueuePath = messageQueuePath ?? throw new ArgumentNullException(nameof(messageQueuePath));
            _messageQueue = new MessageQueue(_messageQueuePath);
            if (!MessageQueue.Exists(_messageQueue.Path))
            {
                MessageQueue.Create(_messageQueue.Path);
            }
        }

        public object GetFirstMessageBody()
        {
            return _messageQueue.Receive()?.Body;
        }

        public void SetFormat(Type[] types)
        {
            _messageQueue.Formatter = new XmlMessageFormatter(types);
        }
    }
}