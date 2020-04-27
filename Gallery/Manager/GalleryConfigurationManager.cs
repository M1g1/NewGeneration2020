using System;
using System.Configuration;

namespace Gallery.Manager
{
    public static class GalleryConfigurationManager
    {

        private const string _pathKeyName = "PathToSave";
        private const string _imageTypeKeyName = "ImageFormat";

        private const string _defaultPathToSave = "/Images/";
        private const string _defaultImageTypes = "image/jpeg; image/png";

        public static string GetSqlConnectionString()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            var sqlConnectionString = connectionStrings["SqlConnection"] ?? throw new ArgumentException("SQL");
            return sqlConnectionString.ConnectionString;
        }

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
}