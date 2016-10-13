// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PokeApp.Helpers
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

        private const string TokenTypeKey = "tokentype_key";
        private static readonly string TokenTypeDefault = string.Empty;

        private const string TokenKey = "token_key";
        private static readonly string TokenDefault = string.Empty;

        #endregion Setting Constants

        public static string TokenType
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(TokenTypeKey, TokenTypeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(TokenTypeKey, value);
            }
        }

        public static string Token
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(TokenKey, TokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(TokenKey, value);
            }
        }
    }
}