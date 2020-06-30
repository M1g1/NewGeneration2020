using System.Collections.Generic;
using Gallery.Config.Manager;

namespace Gallery.MessageQueues
{
    public static class Parser
    {
        public static IDictionary<QueueType, string> ParseQueueNames()
        { 
            var dictionary = new Dictionary<QueueType, string>();

            var uploadImgvalue = GalleryConfigurationManager.GetUploadImageQueueName();

            var uploadMp4value = GalleryConfigurationManager.GetUploadMp4QueueName();

            dictionary.Add(QueueType.UploadImage, uploadImgvalue);

            dictionary.Add(QueueType.UploadMp4, uploadMp4value);

            return dictionary;
        }
    }
}