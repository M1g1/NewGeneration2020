using System.Threading.Tasks;
using Gallery.Service.Contract;

namespace Gallery.Service
{
    public interface IImageService
    {
        string GetTitle(string loadExifPath);
        string GetFileSize(string loadExifPath);
        string GetDateUpload(string loadExifPath);
        string GetManufacturer(string loadExifPath);
        string GetModelOfCamera(string loadExifPath);
        string GetDateCreation(string loadExifPath);
        string FileNameCreation();
        Task<bool> DeleteAsync(string path);
        Task<bool> UploadImageAsync(int userId, byte[] content, string path);
        Task<bool> UploadImageTemporaryAsync(MediaUploadAttemptDto mediaUploadAttemptDto, byte[] content, string path);
        Task<bool> MoveImageFromTempToMainAsync(MessageDto messageDto);
    }
}
