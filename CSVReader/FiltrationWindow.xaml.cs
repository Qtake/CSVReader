using CSVReader.DataBase;
using System;
using System.Windows;

namespace CSVReader
{
    public partial class FiltrationWindow : Window
    {
        private readonly Record? _filter;
        private event Action? DataGridUpdateEvent;

        public FiltrationWindow()
        {
            InitializeComponent();
        }

        public FiltrationWindow(Record filter, Action action) : this()
        {
            _filter = filter;
            Date.SelectedDate = filter.Date;
            Firstname.Text = filter.Firstname;
            Surname.Text = filter.Surname;
            Patronymic.Text = filter.Patronymic;
            City.Text = filter.City;
            Country.Text = filter.Country;
            DataGridUpdateEvent = action;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            _filter!.Date = Date.SelectedDate;
            _filter.Firstname = Firstname.Text;
            _filter.Surname = Surname.Text;
            _filter.Patronymic = Patronymic.Text;
            _filter.City = City.Text;
            _filter.Country = Country.Text;
            CloseWindow();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _filter!.Date = null;
            _filter.Firstname = string.Empty;
            _filter.Surname = string.Empty;
            _filter.Patronymic = string.Empty;
            _filter.City = string.Empty;
            _filter.Country = string.Empty;
            CloseWindow();
        }

        private void CloseWindow()
        {
            DataGridUpdateEvent!.Invoke();
            Close();
        }
    }
}
