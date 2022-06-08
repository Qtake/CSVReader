using System.Windows;

namespace CSVReader.MainMenuElements.Settings
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        public Settings(string[] languages, int defaultLanguageIndex) : this()
        {
            LanguageBox.ItemsSource = languages;
            LanguageBox.SelectedItem = languages[defaultLanguageIndex];
        }
    }
}
