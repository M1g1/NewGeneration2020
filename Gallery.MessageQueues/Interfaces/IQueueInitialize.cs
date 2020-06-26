namespace Gallery.MessageQueues
{
    public interface IQueueInitialize
    {
        /// <summary>
        /// Create queue if not exist.
        /// </summary>
        /// <param name="queueName">Array of queue names</param>
        void CreateIfNotExist(string queueName);
    }
}