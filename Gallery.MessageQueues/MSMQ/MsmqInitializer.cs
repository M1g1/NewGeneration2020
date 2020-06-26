using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MsmqInitializer : IQueueInitialize
    {
        public void CreateIfNotExist(string[] names)
        {
            foreach (var path in names)
            {
                if (!MessageQueue.Exists(path))
                {
                    MessageQueue.Create(path);
                }
            }
        }
    }
}