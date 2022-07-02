using CSVReader.Language;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CSVReader.MainMenuElements.Settings
{
    public partial class SettingsWindow : Window
    {
        private readonly Dictionary<string, Page> _settingsItems;
        private string _selectedKey;

        public SettingsWindow()
        { 
            InitializeComponent();

            _settingsItems = new Dictionary<string, Page>()
            {
                { InterfaceLanguage.Language, new LanguagePage() }
            };

            SettingsMenu.ItemsSource = _settingsItems.Keys;
            _selectedKey = string.Empty;
        }

        private void SettingsMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedKey = SettingsMenu.SelectedValue.ToString()!;
            SelectedPage.Content = _settingsItems[_selectedKey];
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ISaveChanges)_settingsItems[_selectedKey]).SaveChanges();
            }
            catch(InvalidCastException)
            {
                throw new InvalidCastException();
            }
            finally
            {
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
