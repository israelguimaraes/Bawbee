using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Bawbee.Mobile.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion

        public static string UserEmail
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(UserEmail), SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(UserEmail), value);
            }
        }

        public static string UserAcessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(UserAcessToken), SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(UserAcessToken), value);
            }
        }

    }
}
