using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using FileStorageProvider.Interfaces;
using Gallery.DAL;
using Gallery.DAL.Models;
using Gallery.Service.Contract;

namespace Gallery.Service
{
    public class ImageService : IImageService
    {
        private readonly IFileStorage _storage;
        private readonly IMediaRepository _mediaRepo;
        private readonly IUserRepository _userRepo;
        public ImageService()
        {

        }
        public ImageService(IFileStorage storage, IMediaRepository mediaRepo, IUserRepository userRepo)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _mediaRepo = mediaRepo ?? throw new ArgumentNullException(nameof(mediaRepo));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        public string FileNameCreation()
        {
            return string.Format(@"{0}", Guid.NewGuid());
        }

        public async Task<bool> DeleteAsync(string path)
        {
            var directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
                throw new DirectoryNotFoundException(nameof(directoryName));
            if (!File.Exists(path))
                throw new FileNotFoundException(nameof(path));

            var isMediaExist = await _mediaRepo.IsMediaExistByPathAsync(path);
            //
            //If the file already exists in the database by this path
            //and is marked as not deleted, mark it as deleted
            //
            if (isMediaExist)
            {
                var media = await _mediaRepo.GetMediaByPathAsync(path);
                if (!media.IsDeleted)
                {
                    var newMedia = media;
                    newMedia.IsDeleted = !media.IsDeleted;
                    await _mediaRepo.UpdateMediaAsync(media, newMedia);
                }
            }

            return _storage.Delete(path);
        }

        public async Task<bool> UploadImageAsync(int userId, byte[] content, string path)
        {
            var directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            var isMediaExist = await _mediaRepo.IsMediaExistByPathAsync(path);
            //
            //If the file already exists in the database by this path
            //and is marked as deleted, mark it as not deleted
            //
            if (isMediaExist)
            {
                var media = await _mediaRepo.GetMediaByPathAsync(path);
                if (media.IsDeleted)
                {
                    var newMedia = media;
                    newMedia.IsDeleted = !media.IsDeleted;
                    await _mediaRepo.UpdateMediaAsync(media, newMedia);
                }
            }
            //
            //If the file does not exist in the database by this path, add it
            //
            else
            {
                var isUserExist = await _userRepo.IsUserExistAsync(userId);
                if (!isUserExist)
                    return false;
                var extension = Path.GetExtension(path);
                var isMediaTypeExist = await _mediaRepo.IsMediaTypeExistAsync(extension);
                if (!isMediaTypeExist)
                {
                    await _mediaRepo.AddMediaTypeToDatabaseAsync(new MediaType { Type = extension });
                }
                var mediaType = await _mediaRepo.GetMediaTypeAsync(extension);
                await _mediaRepo.AddMediaToDatabaseAsync(new Media
                {
                    Path = path,
                    UserId = userId,
                    MediaTypeId = mediaType.Id
                });
            }
            return _storage.Save(content, path);
        }

        public async Task<bool> UploadImageTemporaryAsync(MediaUploadAttemptDto mediaUploadAttemptDto, byte[] content, string path)
        {
            var directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            await _mediaRepo.AddMediaUploadAttemptToDatabaseAsync(new MediaUploadAttempt
            {
                Label = mediaUploadAttemptDto.Label,
                UserId = mediaUploadAttemptDto.UserId,
                IsInProgress = mediaUploadAttemptDto.IsInProgress,
                IsSuccess = mediaUploadAttemptDto.IsSuccess,
                IpAddress = mediaUploadAttemptDto.IpAddress,
                TimeStamp = mediaUploadAttemptDto.TimeStamp
            });
            return _storage.Save(content, path);
        }

        public string GetTitle(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            return string.IsNullOrEmpty(fileInfo.Name) ? "Data not found" : fileInfo.Name;
        }


        public string GetFileSize(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            string fileSize;

            if (fileInfo.Length >= 1024)
            {
                fileSize = Math.Round((fileInfo.Length / 1024f), 1).ToString() + " KB";
                if ((fileInfo.Length / 1024f) >= 1024f)
                    fileSize = Math.Round((fileInfo.Length / 1024f) / 1024f, 2).ToString() + " MB";
            }
            else
                fileSize = fileInfo.Length.ToString() + " B";

            return fileSize;
        }


        public string GetDateUpload(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            return fileInfo.CreationTime == null ? "Data not found" : fileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss");
        }


        public string GetManufacturer(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            if (!fileInfo.Extension.Equals(".jpg") && !fileInfo.Extension.Equals(".jpeg"))
                return "Data not found";
            using (FileStream fs = new FileStream(loadExifPath, FileMode.Open))
            {
                BitmapSource img = BitmapFrame.Create(fs);

                BitmapMetadata md = (BitmapMetadata)img.Metadata;

                return string.IsNullOrEmpty(md.CameraManufacturer) ? "Data not found" : md.CameraManufacturer;
            }

        }

        public string GetModelOfCamera(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            if (!fileInfo.Extension.Equals(".jpg") && !fileInfo.Extension.Equals(".jpeg"))
                return "Data not found";
            using (FileStream fs = new FileStream(loadExifPath, FileMode.Open))
            {
                BitmapSource img = BitmapFrame.Create(fs);

                BitmapMetadata md = (BitmapMetadata)img.Metadata;

                return string.IsNullOrEmpty(md.CameraModel) ? "Data not found" : md.CameraModel;
            }

        }

        public string GetDateCreation(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            if (!fileInfo.Extension.Equals(".jpg") && !fileInfo.Extension.Equals(".jpeg"))
                return "Data not found";
            using (FileStream fs = new FileStream(loadExifPath, FileMode.Open))
            {
                BitmapSource img = BitmapFrame.Create(fs);

                BitmapMetadata md = (BitmapMetadata)img.Metadata;

                return string.IsNullOrEmpty(md.DateTaken) ? "Data not found" : md.DateTaken;
            }

        }
    }
}
