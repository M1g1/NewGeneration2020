using System;
using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MSMQConsumer : IConsumer
    {
        private const string QUEUEPATH_PREFIX = @".\private$\";
        private T GetFirstMessage<T>(string queueName) where T : class
        {
            var queuePath = string.Concat(QUEUEPATH_PREFIX, queueName);
            var _messageQueue = new MessageQueue(queuePath)
            {
                Formatter = new XmlMessageFormatter(new[] { typeof(string) })
            };
            var msgBodyString = _messageQueue.Receive()?.Body.ToString();
            return Deserializer.DeserializeToObject<T>(msgBodyString);
        }

        public void Consume<T>(string queueName, Action<T> action) where T : class
        {
            action(GetFirstMessage<T>(queueName));
        }
    }
}