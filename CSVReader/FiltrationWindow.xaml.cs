using System.Windows;

namespace CSVReader
{
    /// <summary>
    /// Interaction logic for FiltrationWindow.xaml
    /// </summary>
    public partial class FiltrationWindow : Window
    {
        public FiltrationWindow()
        {
            InitializeComponent();

        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Date.SelectedDate = null;
            Firstname.Text = string.Empty;
            Surname.Text = string.Empty;
            Patronymic.Text = string.Empty;
            City.Text = string.Empty;
            Country.Text = string.Empty;
        }
    }
}
