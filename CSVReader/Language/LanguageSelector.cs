using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace CSVReader.Language
{
    internal class LanguageSelector
    {
        public Dictionary<string, string> Languages { get; private set; }

        public LanguageSelector()
        {
            Languages = new Dictionary<string, string>()
            {
                { "English", "en-US" },
                { "Русский", "ru-RU" }
            };
        }

        public void Select(string key)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Languages[key]);
        }
    }
}
