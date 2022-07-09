namespace CSVReader.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowVM { get; init; }
        public SettingsWindowViewModel SettingsWindowVM { get; init; }

        public ViewModelLocator()
        {
            MainWindowVM = new MainWindowViewModel();
            SettingsWindowVM = new SettingsWindowViewModel();
        }
    }
}
