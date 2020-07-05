using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MSMQPublisher : IPublisher
    {
        private const string QUEUEPATH_PREFIX = @".\private$\";
        public void SendMessage<T>(T message, string queueName) where T : class
        {
            var queuePath = string.Concat(QUEUEPATH_PREFIX, queueName);
            var _messageQueue = new MessageQueue(queuePath)
            {
                Formatter = new XmlMessageFormatter(new[] { typeof(string) })
            };
            var jsonMessage = Serializer.SerializeToJson<T>(message);
            _messageQueue.Send(jsonMessage);
        }
    }
}
