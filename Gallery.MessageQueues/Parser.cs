using System;
using System.Collections.Generic;
using Gallery.Config.Manager;

namespace Gallery.MessageQueues
{
    public static class Parser
    {
        public static IDictionary<QueueType, string> ParseQueueNames()
        {
            var dictionary = new Dictionary<QueueType, string>();

            var queueNameKeyNames = new[]
                {
                    "queues:upload-image",
                    "queues:upload-mp4"
                };

            var queueTypes = (QueueType[])Enum.GetValues(typeof(QueueType));

            for (var index = 0; index < queueTypes.Length; index++)
            {
                dictionary.Add(queueTypes[index], GalleryConfigurationManager.GetAppSettingValue(queueNameKeyNames[index]));
            }

            return dictionary;
        }
    }
}