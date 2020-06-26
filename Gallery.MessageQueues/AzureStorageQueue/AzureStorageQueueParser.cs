using System;
using System.Linq;
using Gallery.Config.Manager;

namespace Gallery.MessageQueues.AzureStorageQueue
{
    public class AzureStorageQueueParser : IQueueParser
    {
        public string[] ParseQueueNames()
        {
            var queueNames = GalleryConfigurationManager.GetAzureMqNames();
            var separator = new[] { "," };
            return queueNames.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }
    }
}