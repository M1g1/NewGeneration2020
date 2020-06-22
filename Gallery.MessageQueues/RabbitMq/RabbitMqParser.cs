using System;
using System.Linq;
using Gallery.Config.Manager;

namespace Gallery.MessageQueues.RabbitMq
{
    public class RabbitMqParser : IQueueParser
    {
        public string[] ParseQueuePaths()
        {
            var queuePaths = GalleryConfigurationManager.GetRabbitMqPaths();
            var separator = new [] {","};
            return queuePaths.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }
    }
}