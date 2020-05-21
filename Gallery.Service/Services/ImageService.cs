using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using FileStorageProvider.Interfaces;

namespace Gallery.Service
{
    public class ImageService : IImageService
    {
        private readonly IFileStorage _storage;

        public ImageService()
        {
            
        }
        public ImageService(IFileStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
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

        public void Delete(string fileToDelete)
        {
            if (File.Exists(fileToDelete))
            {
                File.Delete(fileToDelete);
            }
        }

        public void UploadImage(byte[] content, string path)
        {
            throw new NotImplementedException();
        }
    }
}
