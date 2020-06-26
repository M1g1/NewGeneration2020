namespace Gallery.MessageQueues
{
    public interface IQueueParser
    {
        /// <summary>
        /// Gets queue names from configurations.
        /// </summary>
        /// <returns>Array of queue names.</returns>
        string[] ParseQueueNames();
    }
}