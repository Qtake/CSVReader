using CSVReader.DataBase;
using CSVReader.DataManagers;
using CSVReader.Language;
using CSVReader.MainMenuElements.Settings;
using CSVReader.MainWindowPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Globalization;
using System.Linq;
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
        private IDataManager _dataManager;
        private bool _isDataLoaded;

        public MainWindow()
        {
            string key = ApplicationSettings.Default.LanguageKey;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(new LanguageSelector().GetValue(key));
            InitializeComponent();

            _isDataLoaded = false;
            _dataManager = new FileManager();
        }

        private void DeletePreviousData()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Database.ExecuteSqlRaw("DROP TABLE [Records]");
                context.SaveChanges();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            _isDataLoaded = false;
            OpenItem.IsEnabled = false;
            DeletePreviousData();

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
                _isDataLoaded = true;
                OpenItem.IsEnabled = true;
            }
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (!_isDataLoaded)
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
                OutputDataPage page = (OutputDataPage)MainFrame.Content;
                await Task.Run(() => _dataManager.Write(saveFileDialog.FileName, page.FilteredRecords.ToList()));
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
