using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Gallery.Config.Manager
{
    public static class GalleryConfigurationManager
    {

        private const string _pathKeyName = "PathToSave";
        private const string _pathTempKeyName = "PathToTempSave";
        private const string _imageTypeKeyName = "ImageFormat";
        private const string _sqlConnectionStringKeyName = "SqlConnection";
        private const string _msmqPathsKeyName = "msmq:paths";
        private const string _rabbitmqPathsKeyName = "rabbitmq:paths";
        private const string _rabbitmqConnectionStringKeyName = "RabbitMqConnection";
        private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static readonly ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;

        public static string GetRabbitMqConnectionString()
        {
            var rabbitMqConnectionString = connectionStrings[_rabbitmqConnectionStringKeyName] ?? throw new ArgumentNullException(nameof(connectionStrings));
            return rabbitMqConnectionString.ConnectionString;
        }

        public static string GetRabbitMqPaths()
        {
            return appSettings[_rabbitmqPathsKeyName] ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public static string GetMsmqPaths()
        {
            return appSettings[_msmqPathsKeyName] ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public static string GetSqlConnectionString()
        {
            var sqlConnectionString = connectionStrings[_sqlConnectionStringKeyName] ?? throw new ArgumentNullException(nameof(connectionStrings));
            return sqlConnectionString.ConnectionString;
        }

        public static string GetPathToTempSave()
        {
            return appSettings[_pathTempKeyName] ?? throw new ArgumentNullException(nameof(appSettings));
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