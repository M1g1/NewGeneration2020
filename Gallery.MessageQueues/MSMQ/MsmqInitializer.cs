using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MsmqInitializer : IQueueInitialize
    {
        public void CreateIfNotExist(string queueName)
        {

            if (!MessageQueue.Exists(queueName))
            {
                MessageQueue.Create(queueName);
            }

        }
    }
}