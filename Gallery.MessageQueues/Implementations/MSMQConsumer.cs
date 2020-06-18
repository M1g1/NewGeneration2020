using Newtonsoft.Json;
using System.Messaging;

namespace Gallery.MessageQueues
{
    public class MSMQConsumer : IConsumer
    {
        public T GetFirstMessage<T>(string messageQueuePath) where T : class
        {
            var _messageQueue = new MessageQueue(messageQueuePath)
            {
                Formatter = new XmlMessageFormatter(new[] {typeof(T)})
            };
            var msgBodyObj = _messageQueue.Receive()?.Body;
            var msgBodyString = JsonConvert.SerializeObject(msgBodyObj);
            return JsonConvert.DeserializeObject<T>(msgBodyString);
        }
    }
}