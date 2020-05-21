using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public interface IMediaRepository
    {
        Task AddMediaToDatabaseAsync(Media media);
        Task UpdateDeletionStatus(string path, bool newStatus);
    }
}