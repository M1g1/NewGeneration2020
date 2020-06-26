using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MsmqInitializer : IQueueInitialize
    {
        public void CreateIfNotExist(string[] names)
        {
            foreach (var name in names)
            {
                if (!MessageQueue.Exists(name))
                {
                    MessageQueue.Create(name);
                }
            }
        }
    }
}