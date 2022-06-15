using CSVReader.DataBase;
using CSVReader.MainMenuElements.Settings;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
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

            using (ApplicationContext db = new ApplicationContext())
            {
                Record record = new Record(DateTime.Now, "lexa", "Jevnyak", "Genadevich", "Gomel", "Belarus");
                db.Records.Add(record);
                db.SaveChanges();
                DataBaseRecords.ItemsSource = db.Records.ToList();
            }

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
                Filter = "CSV Files (*.csv)|*.csv"
            };

            bool? dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                string filename = openFileDialog.FileName;
                MessageBox.Show(Path.GetExtension(filename));
            }

            //MessageBox.Show(InterfaceLanguage.OpenFileError, InterfaceLanguage.Error, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                FileName = "Document",
                Filter = "Excel Files | *.xls; *.xlsx; *.xlsm; | XML Files | *.xml"
            };

            bool? dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                string filename = saveFileDialog.FileName;
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
