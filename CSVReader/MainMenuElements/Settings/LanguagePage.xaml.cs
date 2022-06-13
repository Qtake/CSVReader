using CSVReader.Language;
using System.Windows.Controls;

namespace CSVReader.MainMenuElements.Settings
{
    /// <summary>
    /// Interaction logic for LanguagePage.xaml
    /// </summary>
    public partial class LanguagePage : Page, ISaveChanges
    {
        private readonly LanguageSelector _languageSelector;

        public LanguagePage()
        {
            InitializeComponent();

            _languageSelector = new LanguageSelector();
            var keys = _languageSelector.Languages.Keys;
            LanguageBox.ItemsSource = keys;
        }

        public void SaveChanges()
        {
            // save changes into properties file
        }
    }
}
