﻿using CSVReader.Language;
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
using System.Text;
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
        public Page CurrentFramePage { get; private set; }
        public List<Record> DataBaseRecords { get; private set; }
        public bool OpenButtonState { get; private set; }
        public bool SaveAsButtonState { get; private set; }

        private IRepository<Record>? _repository;
        private IReader? _dataReader;
        private IWriter? _dataWriter;
        private const string DefaultFileName = "Document";
        
        public MainWindowViewModel()
        {
            SetDataBaseConnection();

            OpenCommand = new RelayCommand((x) => OpenFile());
            SaveAsCommand = new RelayCommand((x) => SaveAsFile());
            ExitCommand = new RelayCommand((x) => CloseApplication());
            SettingsWindowCommand = new RelayCommand((x) => ShowSettingsWindow());
            FiltrationWindowCommand = new RelayCommand((x) => ShowFiltrationWindow());
            _repository = null;
            _dataReader = null;
            _dataWriter = null;
            CurrentFramePage = null!;
            DataBaseRecords = null!;
            OpenButtonState = true;
            SaveAsButtonState = false;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void SetDataBaseConnection()
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

        private async void OpenFile()
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

        private async void SaveAsFile()
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
                await Task.Run(() => DataBaseRecords = _repository.SelectAll().ToList());
            }
        }
    }
}
