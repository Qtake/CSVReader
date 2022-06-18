using CSVReader.DataBase;
using CSVReader.DataManagers;
using CSVReader.MainMenuElements.Settings;
using CSVReader.MainWindowPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
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
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            InitializeComponent();

            IsDataLoaded = false;
            _dataManager = new FileManager();

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
                MessageBox.Show("Нет данных для сохранения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
               // _dataManager.Write(saveFileDialog.FileName);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
