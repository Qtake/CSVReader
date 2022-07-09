using CSVReader.Language;
using CSVReader.Models.DataBase;
using CSVReader.Models.DataBase.ConnectionManagers;
using CSVReader.Models.DataBase.Repositories;
using CSVReader.Models.DataInteraction;
using CSVReader.Models.DataInteraction.Readers;
using CSVReader.Models.DataInteraction.Writers;
using CSVReader.Views;
using CSVReader.Views.SettingsButton;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSVReader.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand OpenCommand { get; init; }
        public ICommand SaveAsCommand { get; init; }
        public ICommand ExitCommand { get; init; }
        public ICommand SettingsWindowCommand { get; init; }
        public ICommand FiltrationWindowCommand { get; init; }
        public ICommand ApplyFiltersCommand { get; init; }
        public ICommand ResetFiltersCommand { get; init; }
        public Page CurrentFramePage { get; private set; }
        public List<Record> DataBaseRecords { get; private set; }
        public bool OpenButtonState { get; private set; }
        public bool SaveAsButtonState { get; private set; }

        public DateTime? Date
        {
            get => _filter.Date;
            set
            {
                _filter.Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public string Firstname
        {
            get => _filter.Firstname;
            set
            {
                _filter.Firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }

        public string Surname
        {
            get => _filter.Surname;
            set
            {
                _filter.Surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string Patronymic
        {
            get => _filter.Patronymic;
            set
            {
                _filter.Patronymic = value;
                OnPropertyChanged(nameof(Patronymic));
            }
        }

        public string City
        {
            get => _filter.City;
            set
            {
                _filter.City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Country
        {
            get => _filter.Country;
            set
            {
                _filter.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        private IRepository<Record>? _repository;
        private IReader? _dataReader;
        private IWriter? _dataWriter;
        private Record _filter;
        private const string DefaultFileName = "Document";
        
        public MainWindowViewModel()
        {
            SetDataBaseConnection();

            OpenCommand = new RelayCommand((x) => OpenFileAsync());
            SaveAsCommand = new RelayCommand((x) => SaveAsFileAsync());
            ExitCommand = new RelayCommand((x) => CloseApplication());
            SettingsWindowCommand = new RelayCommand((x) => ShowSettingsWindow());
            FiltrationWindowCommand = new RelayCommand((x) => ShowFiltrationWindow());
            ApplyFiltersCommand = new RelayCommand((x) => ApplyFiltersAsync());
            ResetFiltersCommand = new RelayCommand((x) => ResetFiltersAsync());
            _repository = null;
            _dataReader = null;
            _dataWriter = null;
            CurrentFramePage = null!;
            DataBaseRecords = null!;
            OpenButtonState = true;
            SaveAsButtonState = false;
            _filter = new Record();
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private static void SetDataBaseConnection()
        {
            IConnectionManager connectionManager = new ConfigConnection("Local");
            ApplicationSettings.Default.DatabaseConnectionString = connectionManager.GetConnectionString();
        }

        private void DeletePreviousData()
        {
            using (_repository = new MsSqlRepository(ApplicationSettings.Default.DatabaseConnectionString))
            {
                _repository.DeleteAll();
                _repository.Save();
            }
        }

        private void EnableMenuElements(bool flag)
        {
            OpenButtonState = flag;
            SaveAsButtonState = flag;
            OnPropertyChanged(nameof(OpenButtonState));
            OnPropertyChanged(nameof(SaveAsButtonState));
        }

        private void ChangeFramePage(Page page)
        {
            CurrentFramePage = page;
            OnPropertyChanged(nameof(CurrentFramePage));
        }

        private async void OpenFileAsync()
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
                ChangeFramePage(new LoadingPage());
                _dataReader = ReaderSelector.Select(openFileDialog.FileName);
                await _dataReader.ReadAsync(openFileDialog.FileName);
                await GetDataBaseRecordsAsync();
                ChangeFramePage(new OutputDataPage());
                EnableMenuElements(true);
            }

            OpenButtonState = true;
            OnPropertyChanged(nameof(OpenButtonState));
        }

        private async void SaveAsFileAsync()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                FileName = DefaultFileName,
                Filter = "Excel Files | *.xls; *.xlsx; *.xlsm; | XML Files | *.xml"
            };

            bool? dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                _dataWriter = WriterSelector.Select(saveFileDialog.FileName);
                await _dataWriter.WriteAsync(saveFileDialog.FileName, DataBaseRecords);
                MessageBox.Show(InterfaceLanguage.FileSaved,
                                InterfaceLanguage.ApplicationName,
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        private static void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private static void ShowSettingsWindow()
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private static void ShowFiltrationWindow()
        {
            FiltrationWindow filtrationWindow = new FiltrationWindow();
            filtrationWindow.ShowDialog();
        }

        private async Task GetDataBaseRecordsAsync()
        {
            using (_repository = new MsSqlRepository(ApplicationSettings.Default.DatabaseConnectionString))
            {
                DataBaseRecords = await _repository.SelectAll().ToListAsync();
            }
        }

        private async void ApplyFiltersAsync()
        {
            using (_repository = new MsSqlRepository(ApplicationSettings.Default.DatabaseConnectionString))
            {
                IQueryable<Record> records = _repository.SelectAll();

                if (Date != null)
                {
                    records = _repository.SelectAll(x => x.Date == Date);
                }

                if (!string.IsNullOrEmpty(Firstname))
                {
                    records = _repository.SelectAll(x => x.Firstname == Firstname);
                }

                if (!string.IsNullOrEmpty(Surname))
                {
                    records = _repository.SelectAll(x => x.Surname == Surname);
                }

                if (!string.IsNullOrEmpty(Patronymic))
                {
                    records = _repository.SelectAll(x => x.Patronymic == Patronymic);
                }

                if (!string.IsNullOrEmpty(City))
                {
                    records = _repository.SelectAll(x => x.City == City);
                }

                if (!string.IsNullOrEmpty(Country))
                {
                    records = _repository.SelectAll(x => x.Country == Country);
                }

                DataBaseRecords = await records.ToListAsync();
                UpdateSavedRecords();
            }
        }

        private async void ResetFiltersAsync()
        {
            ClearFilterFilds();
            await GetDataBaseRecordsAsync();
            UpdateSavedRecords();
        }

        private void UpdateSavedRecords()
        {
            OnPropertyChanged(nameof(DataBaseRecords));
        }

        private void ClearFilterFilds()
        {
            _filter = new Record();
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Firstname));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Patronymic));
            OnPropertyChanged(nameof(City));
            OnPropertyChanged(nameof(Country));
        }
    }
}
