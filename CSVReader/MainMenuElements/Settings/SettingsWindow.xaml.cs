using CSVReader.Language;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CSVReader.MainMenuElements.Settings
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public Dictionary<string, Page> SettingsItems { get; private set; }
        public string[] Strings { get; set; }

        private string _selectedSettingsItem;

        public SettingsWindow()
        {
            SettingsItems = new Dictionary<string, Page>()
            {
                { InterfaceLanguage.Language, new LanguagePage() }
            };

            Strings = new string[] { "hehe", "haha" };

            InitializeComponent();



            //_selectedSettingsItem = string.Empty;
            //test.ItemsSource = SettingsItems.Keys;
        }

        private void test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //_selectedSettingsItem = test.SelectedItem.ToString() ?? "None";
            //SelectedPage.Content = SettingsItems[_selectedSettingsItem];
        }
    }
}
