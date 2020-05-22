using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public interface IMediaRepository
    {
        Task<bool> IsMediaExistByPathAsync(string path);
        Task AddMediaToDatabaseAsync(Media media);
        Task<Media> GetMediaByPathAsync(string path);
        Task UpdateMediaDeletionStatusAsync(Media media, bool newStatus);

        Task<bool> IsMediaTypeExistAsync(string extension);
        Task AddMediaTypeToDatabaseAsync(MediaType mediaType);
        Task<MediaType> GetMediaTypeAsync(string extension);
    }
}