namespace Gallery
{
    public interface IPublisher
    {
        void SendMessage(object message, string label);
    }
}