using System;
using System.Linq;
using Gallery.Config.Manager;

namespace Gallery.MessageQueues.RabbitMq
{
    public class RabbitMqParser : IQueueParser
    {
        public string[] ParseQueueNames()
        {
            var queueNames = GalleryConfigurationManager.GetRabbitMqNames();
            var separator = new [] {","};
            return queueNames.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }
    }
}