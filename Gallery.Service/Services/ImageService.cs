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

        //
        // check for equality of pictures
        // Input: Bitmap1, Bitmap2
        // Output:
        //        true - is equal
        //        false - isn't equal
        //
        public bool CompareBitmaps(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1 == null || bmp2 == null)
                return false;
            if (object.Equals(bmp1, bmp2))
                return true;
            if (!bmp1.Size.Equals(bmp2.Size) || !bmp1.PixelFormat.Equals(bmp2.PixelFormat))
                return false;

            int bytes = bmp1.Width * bmp1.Height * (Image.GetPixelFormatSize(bmp1.PixelFormat) / 8);

            bool result = true;
            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bitmapData1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width, bmp1.Height), ImageLockMode.ReadOnly, bmp1.PixelFormat);
            BitmapData bitmapData2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadOnly, bmp2.PixelFormat);

            Marshal.Copy(bitmapData1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bitmapData2.Scan0, b2bytes, 0, bytes);

            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1bytes[n] != b2bytes[n])
                {
                    result = false;
                    break;
                }
            }

            bmp1.UnlockBits(bitmapData1);
            bmp2.UnlockBits(bitmapData2);

            return result;
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
