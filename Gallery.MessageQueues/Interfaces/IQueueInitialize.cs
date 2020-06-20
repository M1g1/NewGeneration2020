namespace Gallery.MessageQueues
{
    public interface IQueueInitialize
    {
        void CreateIfNotExist(string[] paths);
    }
}