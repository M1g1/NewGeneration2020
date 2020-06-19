using System.Messaging;
using Newtonsoft.Json;

namespace Gallery.MessageQueues
{
    public class MSMQPublisher : IPublisher
    {
        public void SendMessage<T>(T message, string queuePath) where T : class
        {
            var _messageQueue = new MessageQueue(queuePath)
            {
                Formatter = new XmlMessageFormatter(new[] { typeof(string) })
            };
            var jsonMessage = JsonConvert.SerializeObject(message);
            _messageQueue.Send(jsonMessage);
        }
    }
}
