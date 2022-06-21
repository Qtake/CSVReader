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

        public string[] GetNames()
        {
            return _languages.Keys.ToArray();
        }

        public string GetValue(string name)
        {
            return _languages[name];
        }

        public void SetCurrentThreadLanguage(string value)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_languages[value]);
        }
    }
}
