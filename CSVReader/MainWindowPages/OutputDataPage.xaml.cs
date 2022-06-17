using CSVReader.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Controls;

namespace CSVReader.MainWindowPages
{
    /// <summary>
    /// Interaction logic for OutputDataPage.xaml
    /// </summary>
    public partial class OutputDataPage : Page
    {
        public OutputDataPage()
        {
            InitializeComponent();

            using (ApplicationContext db = new ApplicationContext())
            {
                Record record = new Record(DateTime.Now, "Kolya", "debik", "Debik", "Gomel", "Belarus");
                db.Records.Add(record);
                db.SaveChanges();
                DataGrid.ItemsSource = db.Records.ToList();
            }
        }

        private void Filtration_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FiltrationWindow filtrationWindow = new FiltrationWindow();
            filtrationWindow.ShowDialog();
        }
    }
}
