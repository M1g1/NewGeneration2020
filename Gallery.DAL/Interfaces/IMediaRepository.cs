using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public interface IMediaRepository
    {
        Task<bool> IsMediaExistByPathAsync(string path);
        Task AddMediaToDatabaseAsync(Media media);
        Task<Media> GetMediaByPath(string path);
        Task UpdateMediaDeletionStatusAsync(Media media, bool newStatus);
    }
}