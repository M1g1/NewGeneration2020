using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MsmqInitializer : IQueueInitialize
    {
        public void CreateIfNotExist(string[] paths)
        {
            foreach (var path in paths)
            {
                if (!MessageQueue.Exists(path))
                {
                    MessageQueue.Create(path);
                }
            }
        }
    }
}