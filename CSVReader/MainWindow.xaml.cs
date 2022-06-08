using CSVReader.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSVReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LanguageSelector _languageSelector;
        private string[] _languages;

        private const int DefaultLangeageIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            _languageSelector = new LanguageSelector();
            _languages = _languageSelector.Languages.Keys.ToArray();
            _languageSelector.Select(_languages[DefaultLangeageIndex]);

            Settings.Header = InterfaceLanguage.Settings;
            Language1.Header = InterfaceLanguage.Language;
        }

        private void Language_Click(object sender, RoutedEventArgs e)
        {
            MainMenuElements.Settings.Settings settingsWindow = new MainMenuElements.Settings.Settings(_languages, DefaultLangeageIndex);
            settingsWindow.ShowDialog();
        }
    }
}
