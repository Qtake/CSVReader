using CSVReader.Language;
using CSVReader.MainMenuElements.Settings;
using Microsoft.Win32;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace CSVReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            InitializeComponent();

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                FileName = "Document",
                Filter = "CSV files (*.csv)|*.csv"
            };

            bool? isOpen = openFileDialog.ShowDialog();

            if (isOpen == true)
            {
                string filename = openFileDialog.FileName;
                MessageBox.Show(filename);
            }
            else
            {
                MessageBox.Show(InterfaceLanguage.OpenFileError, InterfaceLanguage.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                FileName = "Document",
                Filter = "Excel Files | *.xls; *.xlsx; *.xlsm; | XML Files | *.xml"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                MessageBox.Show(filename);
                //Path.GetExtension(filename)
            }
            else
            {
                MessageBox.Show(InterfaceLanguage.SaveFileError, InterfaceLanguage.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
