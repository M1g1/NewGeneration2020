using System.Threading.Tasks;

namespace Gallery.Worker
{
    public interface IWork
    {
        Task StartAsync();
        void Stop();
    }
}