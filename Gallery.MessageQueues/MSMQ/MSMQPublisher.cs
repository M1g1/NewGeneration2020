using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MSMQPublisher : IPublisher
    {
        public void SendMessage<T>(T message, string queueName) where T : class
        {
            var _messageQueue = new MessageQueue(queueName)
            {
                Formatter = new XmlMessageFormatter(new[] { typeof(string) })
            };
            var jsonMessage = Serializer.SerializeToJson<T>(message);
            _messageQueue.Send(jsonMessage);
        }
    }
}
