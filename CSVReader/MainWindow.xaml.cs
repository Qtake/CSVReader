using CSVReader.DataBase;
using CSVReader.DataManagers;
using CSVReader.Language;
using CSVReader.MainMenuElements.Settings;
using CSVReader.MainWindowPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CSVReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsDataLoaded { get; set; }
        private IDataManager _dataManager;

        public MainWindow()
        {
            string key = ApplicationSettings.Default.LanguageKey;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(new LanguageSelector().GetValue(key));
            InitializeComponent();

            IsDataLoaded = false;
            _dataManager = new FileManager();
        }

        private void DeletePreviousData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.ExecuteSqlRaw("DROP TABLE [Records]");
                db.SaveChanges();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            IsDataLoaded = false;

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                FileName = "Document",
                Filter = "CSV Files (*.csv)|*.csv"
            };

            bool? dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                MainFrame.Content = new LoadingPage();
                await Task.Run(() => _dataManager.Read(openFileDialog.FileName));
                MainFrame.Content = new OutputDataPage();
                IsDataLoaded = true;
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (!IsDataLoaded)
            {
                MessageBox.Show(InterfaceLanguage.NoDataToSave, InterfaceLanguage.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                FileName = "Document",
                Filter = "Excel Files | *.xls; *.xlsx; *.xlsm; | XML Files | *.xml"
            };

            bool? dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                List<Record> records = new List<Record>()
                {
                    new Record(DateTime.Now, "Lexa", "J", "G", "Gomel", "Belarus"),
                    new Record(DateTime.Now, "Raul", "K", "A", "Gomel", "Belarus")
                };

               _dataManager.Write(saveFileDialog.FileName, records);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
