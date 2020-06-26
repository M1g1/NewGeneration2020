using System;
using System.Linq;
using Gallery.Config.Manager;

namespace Gallery.MessageQueues.MSMQ
{
    public class MsmqParser : IQueueParser
    {
        public string[] ParseQueueNames()
        {
            var queueNames = GalleryConfigurationManager.GetMsmqNames();
            var separator = new [] {","};
            return queueNames.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }
    }
}