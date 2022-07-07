namespace CSVReader.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowVM { get; init; }
        public OutputPageViewModel OutputPageVM { get; init; }

        public ViewModelLocator()
        {
            MainWindowVM = new MainWindowViewModel();
            OutputPageVM = new OutputPageViewModel();
        }
    }
}
