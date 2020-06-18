using System.Messaging;

namespace Gallery.MessageQueues
{
    public static class MsmqInitializer
    {
        public static void CreateIfNotExist(string[] paths)
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