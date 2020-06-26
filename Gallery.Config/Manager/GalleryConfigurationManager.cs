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
        private const string _msmqNamesKeyName = "msmq:names";
        private const string _rabbitmqNamesKeyName = "rabbitmq:names";
        private const string _azuremqNamesName = "azuremq:names";
        private const string _rabbitmqConnectionStringKeyName = "RabbitMqConnection";
        private const string _azuremqConnectionStringKeyName = "AzureMqConnection";
        private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static readonly ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;

        public static string GetAzureMqConnectionString()
        {
            var azuremqConnectionString = connectionStrings[_azuremqConnectionStringKeyName] ?? throw new ArgumentNullException(nameof(connectionStrings));
            return azuremqConnectionString.ConnectionString;
        }

        public static string GetAzureMqNames()
        {
            return appSettings[_azuremqNamesName] ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public static string GetRabbitMqConnectionString()
        {
            var rabbitMqConnectionString = connectionStrings[_rabbitmqConnectionStringKeyName] ?? throw new ArgumentNullException(nameof(connectionStrings));
            return rabbitMqConnectionString.ConnectionString;
        }

        public static string GetRabbitMqNames()
        {
            return appSettings[_rabbitmqNamesKeyName] ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public static string GetMsmqNames()
        {
            return appSettings[_msmqNamesKeyName] ?? throw new ArgumentNullException(nameof(appSettings));
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