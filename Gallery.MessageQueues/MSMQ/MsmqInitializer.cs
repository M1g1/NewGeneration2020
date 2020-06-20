using System.Messaging;

namespace Gallery.MessageQueues.MSMQ
{
    public class MsmqInitializer
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