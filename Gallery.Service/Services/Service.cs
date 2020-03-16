﻿using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media.Imaging;

namespace Gallery.Service
{
    public class GalleryConfigurationManager
    {
        private const string _pathKeyName = "PathToSave";
        private const string _imageTypeKeyName = "ImageFormat";

        private const string _defaultPathToSave = "/Images/";
        private const string _defaultImageTypes = "image/jpeg; image/png";


        public static string GetPathToSave()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string _pathToSave = _defaultPathToSave;
            if (!string.IsNullOrEmpty(appSettings[_pathKeyName]))
            {
                _pathToSave = appSettings[_pathKeyName] + "/";
            }
            return _pathToSave;
        }


        public static string GetAvailableImageTypes()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string _imageTypes = _defaultImageTypes;
            if (!string.IsNullOrEmpty(appSettings[_imageTypeKeyName]))
            {
                _imageTypes = appSettings[_imageTypeKeyName];
            }
            return _imageTypes;
        }

    }

    public class Picture
    {
        private string title;
        private string manufacturer;
        private string modelOfCamera;
        private string fileSize;
        private string dateCreation;
        private string dateUpload;
        public string Title { get => title; }
        public string Manufacturer { get => manufacturer; }
        public string ModelOfCamera { get => modelOfCamera; }
        public string FileSize { get => fileSize; }
        public string DateCreation { get => dateCreation; }
        public string DateUpload { get => dateUpload; }

        public void LoadExifData(string LoadExifPath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(LoadExifPath);

                if (fileInfo.Extension.Equals(".jpg") || fileInfo.Extension.Equals(".jpeg"))
                {
                    FileStream fs = new FileStream(LoadExifPath, FileMode.Open);

                    BitmapSource img = BitmapFrame.Create(fs);

                    BitmapMetadata md = (BitmapMetadata)img.Metadata;
                    //
                    //manufacturer from EXIF
                    if (string.IsNullOrEmpty(md.CameraManufacturer))
                        manufacturer = "Data not found";
                    else
                        manufacturer = md.CameraManufacturer;

                    //
                    //modelOfCamera from EXIF
                    if (string.IsNullOrEmpty(md.CameraModel))
                        modelOfCamera = "Data not found";
                    else
                        modelOfCamera = md.CameraModel;

                    //
                    //DateCreation from EXIF
                    if (string.IsNullOrEmpty(md.DateTaken))
                        dateCreation = "Data not found";
                    else
                        dateCreation = md.DateTaken;

                    fs.Close();
                }
                else
                {
                    manufacturer = "Data not found";
                    modelOfCamera = "Data not found";
                    dateCreation = "Data not found";
                }

                //
                //title from FileInfo
                if (string.IsNullOrEmpty(fileInfo.Name))
                    title = "Data not found";
                else
                    title = fileInfo.Name;

                //
                //FileSize from FileInfo


                if (fileInfo.Length >= 1024)
                {
                    fileSize = Math.Round((fileInfo.Length / 1024f), 1).ToString() + " KB";
                    if ((fileInfo.Length / 1024f) >= 1024f)
                        fileSize = Math.Round((fileInfo.Length / 1024f) / 1024f, 2).ToString() + " MB";
                }
                else
                    fileSize = fileInfo.Length.ToString() + " B";


                //
                //DateUpload from FileInfo
                if (fileInfo.CreationTime == null)
                    dateUpload = "Data not found";
                else
                    dateUpload = fileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss");


            }
            catch (Exception err)
            {

                // need to  errors
            }
        }
    }

    public class Servises
    {
        //
        // Hash-Function
        // Input: String
        // Otput: String with ShaHash
        //
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }



        //
        // check for equality of pictures
        // Input: Bitmap1, Bitmap2
        // Output:
        //        true - is equal
        //        false - isn't equal
        //
        public static bool CompareBitmapsFast(Bitmap bmp1, Bitmap bmp2)
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

    }
}