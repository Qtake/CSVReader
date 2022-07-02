using CSVReader.Language;
using System.Windows;
using System.Windows.Controls;

namespace CSVReader.MainMenuElements.Settings
{
    public partial class LanguagePage : Page, ISaveChanges
    {
        private readonly LanguageSelector _languageSelector;

        public LanguagePage()
        {
            InitializeComponent();

            _languageSelector = new LanguageSelector();
            FillLanguageBox();
        }

        private void FillLanguageBox()
        {
            LanguageBox.ItemsSource = _languageSelector.GetNames();
            string selectedLanguage = ApplicationSettings.Default.LanguageName;
            LanguageBox.SelectedValue = selectedLanguage;
        }

        public void SaveChanges()
        {
            string selectedLanguage = LanguageBox.SelectedValue.ToString()!;
            ApplicationSettings.Default.LanguageName = selectedLanguage;
            ApplicationSettings.Default.Save();
            MessageBox.Show(InterfaceLanguage.NeedRestart, InterfaceLanguage.Settings, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
