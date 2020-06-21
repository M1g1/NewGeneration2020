using System;
using System.Linq;
using Gallery.Config.Manager;

namespace Gallery.MessageQueues.MSMQ
{
    public class MsmqParser : IQueueParser
    {
        public string[] ParseQueuePaths()
        {
            var queuePaths = GalleryConfigurationManager.GetMsmqPaths();
            var separator = new [] {","};
            return queuePaths.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim()).ToArray();
        }
    }
}