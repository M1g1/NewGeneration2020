using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Gallery.Manager
{
    public static class GalleryConfigurationManager
    {

        private const string _pathKeyName = "PathToSave";
        private const string _imageTypeKeyName = "ImageFormat";
        private const string _connectionStringKeyName = "SqlConnection";
        private const string _messageQueuingKeyName = "MessageQueuingName";
        private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static readonly ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;

        public static string GetMessageQueuingName()
        {
            return appSettings[_messageQueuingKeyName] ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public static string GetSqlConnectionString()
        {
            var sqlConnectionString = connectionStrings[_connectionStringKeyName] ?? throw new ArgumentNullException(nameof(connectionStrings));
            return sqlConnectionString.ConnectionString;
        }

        public static string GetPathToSave()
        {
            return appSettings[_pathKeyName] ?? throw new ArgumentNullException(nameof(appSettings));
        }


        public static string GetAvailableImageTypes()
        {
            return appSettings[_imageTypeKeyName] ?? throw new ArgumentNullException(nameof(appSettings));
        }

    }
}