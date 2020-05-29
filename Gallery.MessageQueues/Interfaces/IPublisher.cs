namespace Gallery.MessageQueues
{
    public interface IPublisher
    {
        void SendMessage(object message, string label);
    }
}