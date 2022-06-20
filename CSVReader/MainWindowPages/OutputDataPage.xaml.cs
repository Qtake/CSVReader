using CSVReader.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace CSVReader.MainWindowPages
{
    /// <summary>
    /// Interaction logic for OutputDataPage.xaml
    /// </summary>
    public partial class OutputDataPage : Page
    {
        public IEnumerable<Record> FilteredRecords { get; private set; }
        private readonly List<Record> _dataBaseRecords;
        private readonly Record _filter;
        private event Action? DataGridUpdateEvent;

        public OutputDataPage()
        {
            InitializeComponent();

            using (ApplicationContext context = new ApplicationContext())
            {
                _dataBaseRecords = context.Records.ToList();
            }

            FilteredRecords = _dataBaseRecords;
            _filter = new Record();
            DataGridUpdateEvent = null;
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            if (_filter.Date != null)
            {
                FilteredRecords = FilteredRecords.Where(x => x.Date == _filter.Date);
            }

            if (!string.IsNullOrEmpty(_filter.Name))
            {
                FilteredRecords = FilteredRecords.Where(x => x.Name == _filter.Name);
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

            DataGrid.ItemsSource = FilteredRecords;
        }

        private void Filtration_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FilteredRecords = _dataBaseRecords;
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
        }
    }
}
