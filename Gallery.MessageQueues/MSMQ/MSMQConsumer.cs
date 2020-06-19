﻿using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MSMQConsumer : IConsumer
    {
        public T GetFirstMessage<T>(string messageQueuePath) where T : class
        {
            var _messageQueue = new MessageQueue(messageQueuePath)
            {
                Formatter = new XmlMessageFormatter(new[] { typeof(string) })
            };
            var msgBodyString = _messageQueue.Receive()?.Body.ToString();
            return Deserializer.DeserializeToObject<T>(msgBodyString);
        }
    }
}