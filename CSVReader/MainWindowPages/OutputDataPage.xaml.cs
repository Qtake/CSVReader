using CSVReader.DataBase;
using CSVReader.DataBase.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Controls;

namespace CSVReader.MainWindowPages
{
    public partial class OutputDataPage : Page
    {
        public IQueryable<Record> FilteredRecords { get; private set; }
        private readonly IRepository<Record> _repository;
        private readonly Record _filter;
        private event Action? DataGridUpdateEvent;

        public OutputDataPage()
        {
            InitializeComponent();

            _repository = new MsSqlRepository(ApplicationSettings.Default.DatabaseConnectionString);
            FilteredRecords = _repository.SelectAll();
            _filter = new Record();
            DataGridUpdateEvent = null;

            FillDataGrid();
        }

        private async void FillDataGrid()
        {
            FilteredRecords = _repository.SelectAll(_filter);
            DataGrid.ItemsSource = await FilteredRecords.ToListAsync();
        }

        private void Filtration_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FilteredRecords = _repository.SelectAll();
            FiltrationWindow filtrationWindow = new FiltrationWindow(_filter, DataGridUpdateEvent ?? FillDataGrid);
            filtrationWindow.ShowDialog();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridUpdateEvent += FillDataGrid;
        }

        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridUpdateEvent -= FillDataGrid;
            _repository.Dispose();
        }
    }
}
