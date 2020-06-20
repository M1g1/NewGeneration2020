using System;
using System.Linq;
using Gallery.Config.Manager;

namespace Gallery.MessageQueues
{
    public static class Parser
    {
        public static string[] ParseMsmqPaths()
        {
            var queuePaths = GalleryConfigurationManager.GetMsmqPaths();
            var separator = new [] {","};
            return queuePaths.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim()).ToArray();
        }
    }
}