using System;
using System.Globalization;
using System.Threading;
using WebApi.Common.Logic.Properties;

namespace WebApi.Common.Logic.Helpers
{
    /// <summary>
    /// Main class to set the app language.
    /// </summary>
    public static class Language
    {
        public static string AppLanguage { get; set; }

        public static void InitializeLanguage()
        {
            //ConfigurationManager.AppSettings[]
            var idioma = Environment.GetEnvironmentVariable(ConfigStrings.Language, EnvironmentVariableTarget.User);
            
            if (String.IsNullOrEmpty(idioma))
            {
                AppLanguage = ConfigStrings.Spanish;
                Environment.SetEnvironmentVariable(ConfigStrings.Language, ConfigStrings.Spanish, EnvironmentVariableTarget.User);
                ChangeLanguage(ConfigStrings.Spanish);
            }
            else
            {
                AppLanguage = idioma;
                ChangeLanguage(idioma);                
            }            
        }
        public static void ChangeLanguage(string idioma)
        {
            Environment.SetEnvironmentVariable(ConfigStrings.Language, idioma, EnvironmentVariableTarget.User);
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(idioma);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(idioma);
        }
    }
}
