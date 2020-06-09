using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Gallery.Worker.Manager
{
    public static class GalleryWorkerConfigurationManager
    {
        private const string _connectionStringKeyName = "SqlConnection";
        private const string _messageQueuingKeyName = "MessageQueuingPath";
        private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static readonly ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;

        public static string GetMessageQueuingPath()
        {
            return appSettings[_messageQueuingKeyName] ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public static string GetSqlConnectionString()
        {
            var sqlConnectionString = connectionStrings[_connectionStringKeyName] ?? throw new ArgumentNullException(nameof(connectionStrings));
            return sqlConnectionString.ConnectionString;
        }

    }
}