using System.Drawing;

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
        bool CompareBitmaps(Bitmap bmp1, Bitmap bmp2);
        void Delete(string fileToDelete);
        void UploadImage(byte[] _img, string _pathToSave);
    }
}
