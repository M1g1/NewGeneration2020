using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Gallery.Config.Manager
{
    public static class GalleryConfigurationManager
    {
        private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static readonly ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;

        public static string GetConnectionString(string name)
        {
            var connectionStringSettings = connectionStrings[name] ?? throw new ArgumentNullException(nameof(connectionStrings));
            return connectionStringSettings.ConnectionString;
        }

        public static string GetAppSettingValue(string keyName)
        {
            return appSettings[keyName] ?? throw new ArgumentNullException(nameof(appSettings));
        }
    }
}