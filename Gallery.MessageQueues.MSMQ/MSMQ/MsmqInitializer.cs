using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MsmqInitializer : IQueueInitialize
    {
        private const string QUEUEPATH_PREFIX = @".\private$\";
        public void CreateIfNotExist(string queueName)
        {
            var queuePath = string.Concat(QUEUEPATH_PREFIX, queueName);
            if (!MessageQueue.Exists(queuePath))
            {
                MessageQueue.Create(queuePath);
            }

        }
    }
}