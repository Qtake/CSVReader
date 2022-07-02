using CSVReader.DataBase.ConnectionManagers;
using CSVReader.DataBase.Repositories;
using CSVReader.DataInteraction.Readers;
using CSVReader.DataInteraction.Writers;
using CSVReader.DataManagers;
using CSVReader.Language;
using CSVReader.MainMenuElements.Settings;
using CSVReader.MainWindowPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Windows;

namespace CSVReader
{
    public partial class MainWindow : Window
    {
        private IReader? _dataReader;
        private IWriter? _dataWriter;
        private IConnectionManager? _connection;
        private const string DefaultFileName = "Document";

        public MainWindow()
        {
            string name = ApplicationSettings.Default.LanguageName;
            LanguageSelector languageSelector = new LanguageSelector();
            languageSelector.SetCurrentThreadLanguage(name);

            InitializeComponent();
            SetDatabaseConnection();

            _dataReader = null;
            _dataWriter = null;
            _connection = null;
            SaveAsItem.IsEnabled = false;
        }

        private void SetDatabaseConnection()
        {
            _connection = new ConfigConnection("Local");
            ApplicationSettings.Default.DatabaseConnectionString = _connection.GetConnectionString();
        }

        private void DeletePreviousData()
        {
            using (MsSqlRepository repository = new MsSqlRepository(ApplicationSettings.Default.DatabaseConnectionString))
            {
                repository.DeleteAll();
                repository.Save();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            EnableMenuElements(false);
            DeletePreviousData();

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                FileName = DefaultFileName,
                Filter = "CSV Files (*.csv)|*.csv"
            };

            bool? dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                MainFrame.Content = new LoadingPage();
                _dataReader = ReaderSelector.Select(openFileDialog.FileName);
                await _dataReader.ReadAsync(openFileDialog.FileName);
                MainFrame.Content = new OutputDataPage();
                EnableMenuElements(true);
            }

            OpenItem.IsEnabled = true;
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            EnableMenuElements(false);

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                FileName = DefaultFileName,
                Filter = "Excel Files | *.xls; *.xlsx; *.xlsm; | XML Files | *.xml"
            };

            bool? dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                OutputDataPage page = (OutputDataPage)MainFrame.Content;
                _dataWriter = WriterSelector.Select(saveFileDialog.FileName);
                var records = await page.FilteredRecords.ToListAsync();
                await _dataWriter.WriteAsync(saveFileDialog.FileName, records);
                MessageBox.Show(InterfaceLanguage.FileSaved,
                                System.Reflection.Assembly.GetCallingAssembly().GetName().Name,
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }

            EnableMenuElements(true);
        }

        private void EnableMenuElements(bool flag)
        {
            OpenItem.IsEnabled = flag;
            SaveAsItem.IsEnabled = flag;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
