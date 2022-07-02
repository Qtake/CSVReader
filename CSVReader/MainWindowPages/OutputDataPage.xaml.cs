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
            if (_filter.Date != null)
            {
                FilteredRecords = FilteredRecords.Where(x => x.Date == _filter.Date);
            }

            if (!string.IsNullOrEmpty(_filter.Firstname))
            {
                FilteredRecords = FilteredRecords.Where(x => x.Firstname == _filter.Firstname);
            }

            if (!string.IsNullOrEmpty(_filter.Surname))
            {
                FilteredRecords = FilteredRecords.Where(x => x.Surname == _filter.Surname);
            }

            if (!string.IsNullOrEmpty(_filter.Patronymic))
            {
                FilteredRecords = FilteredRecords.Where(x => x.Patronymic == _filter.Patronymic);
            }

            if (!string.IsNullOrEmpty(_filter.City))
            {
                FilteredRecords = FilteredRecords.Where(x => x.City == _filter.City);
            }

            if (!string.IsNullOrEmpty(_filter.Country))
            {
                FilteredRecords = FilteredRecords.Where(x => x.Country == _filter.Country);
            }

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
