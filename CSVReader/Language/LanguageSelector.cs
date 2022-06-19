using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace CSVReader.Language
{
    internal class LanguageSelector
    {
        private readonly Dictionary<string, string> _languages;

        public LanguageSelector()
        {
            _languages = new Dictionary<string, string>()
            {
                { "English", "en-US" },
                { "Русский", "ru-RU" }
            };
        }

        public string[] GetKeys()
        {
            return _languages.Keys.ToArray();
        }

        public string GetValue(string key)
        {
            return _languages[key];
        }

        public void SetLanguage(string key)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_languages[key]);
        }
    }
}
