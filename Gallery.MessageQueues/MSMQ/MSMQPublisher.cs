using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MSMQPublisher : IPublisher
    {
        public void SendMessage<T>(T message, string queuePath) where T : class
        {
            var _messageQueue = new MessageQueue(queuePath)
            {
                Formatter = new XmlMessageFormatter(new[] { typeof(string) })
            };
            var jsonMessage = Serializer.SerializeToJson<T>(message);
            _messageQueue.Send(jsonMessage);
        }
    }
}
