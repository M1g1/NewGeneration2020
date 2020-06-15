using System.Threading.Tasks;

namespace Gallery.Worker.Interfaces
{
    public interface IWork
    {
        Task StartAsync();
        void Stop();
    }
}