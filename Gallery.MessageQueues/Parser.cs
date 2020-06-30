using System.Collections.Generic;
using Gallery.Config.Manager;

namespace Gallery.MessageQueues
{
    public static class Parser
    {
        public static IDictionary<QueueType, string> ParseQueueNames()
        { 
            var dictionary = new Dictionary<QueueType, string>();

            var uploadImgQueueNameKeyName = "queues:upload-image";

            var uploadMp4QueueNameKeyName = "queues:upload-mp4";

            var uploadImgvalue = GalleryConfigurationManager.GetAppSettingValue(uploadImgQueueNameKeyName);

            var uploadMp4value = GalleryConfigurationManager.GetAppSettingValue(uploadMp4QueueNameKeyName);

            dictionary.Add(QueueType.UploadImage, uploadImgvalue);

            dictionary.Add(QueueType.UploadMp4, uploadMp4value);

            return dictionary;
        }
    }
}