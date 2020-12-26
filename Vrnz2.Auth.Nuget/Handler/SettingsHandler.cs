using Vrnz2.Auth.Nuget.Settings;

namespace Vrnz2.Auth.Nuget.Handler
{
    public class SettingsHandler
    {
        #region Variables

        private static SettingsHandler _instance;

        #endregion

        #region Constructors

        private SettingsHandler(AuthSettings authSettings) 
            => AuthSettings = authSettings;

        #endregion

        #region Attributes

        public static void Init(AuthSettings authSettings)
        {
            _instance = _instance ?? new SettingsHandler(authSettings);
        }

        public static SettingsHandler Instance
            => _instance;

        public AuthSettings AuthSettings { get; }

        #endregion
    }
}
