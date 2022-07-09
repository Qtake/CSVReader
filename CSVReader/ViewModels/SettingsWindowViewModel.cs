using CSVReader.Language;
using CSVReader.Views.SettingsButton;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSVReader.ViewModels
{
    internal class SettingsWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand OpenLanguagePageCommand { get; init; }
        public ICommand? CurrentPageSaveChangesCommand { get; private set; }
        public Page CurrentFramePage { get; private set; }
        public string[] LanguageNames { get; private set; }
        public string SelectedLanguageName 
        {
            get => _selectedLanguageName;
            set
            {
                _selectedLanguageName = value;
                OnPropertyChanged(nameof(SelectedLanguageName));
            }
        }

        private readonly LanguageSelector _languageSelector;
        private string _selectedLanguageName;

        public SettingsWindowViewModel()
        {
            OpenLanguagePageCommand = new RelayCommand((x) => OpenLanguagePage());
            CurrentPageSaveChangesCommand = null;
            CurrentFramePage = null!;
            _languageSelector = new LanguageSelector();
            LanguageNames = _languageSelector.GetNames();
            _selectedLanguageName = ApplicationSettings.Default.LanguageName;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void ChangeFramePage(Page page, Action saveChangesCommand)
        {
            CurrentFramePage = page;
            OnPropertyChanged(nameof(CurrentFramePage));
            CurrentPageSaveChangesCommand = new RelayCommand((x) => saveChangesCommand());
            OnPropertyChanged(nameof(CurrentPageSaveChangesCommand));
        }

        private void OpenLanguagePage()
        {
            ChangeFramePage(new LanguagePage(), SaveLanguagePageChanges);
        }

        private void SaveLanguagePageChanges()
        {
            ApplicationSettings.Default.LanguageName = SelectedLanguageName;
            ApplicationSettings.Default.Save();
            MessageBox.Show(InterfaceLanguage.NeedRestart, InterfaceLanguage.Settings, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
