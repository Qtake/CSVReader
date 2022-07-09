using System;

namespace CSVReader.Models.DataBase
{
    [Serializable]
    public class Record
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Record()
        {
            Date = null;
            Firstname = string.Empty;
            Surname = string.Empty;
            Patronymic = string.Empty;
            City = string.Empty;
            Country = string.Empty;
        }

        public Record(DateTime date, string name, string surname, string patronymic, string city, string country)
        {
            Date = date;
            Firstname = name;
            Surname = surname;
            Patronymic = patronymic;
            City = city;
            Country = country;
        }

        public bool ParseRecord(string fileLine)
        {
            try
            {
                string[] splitedData = fileLine.Split(';');

                Id = 0;
                Date = Convert.ToDateTime(splitedData[0]);
                Firstname = splitedData[1];
                Surname = splitedData[2];
                Patronymic = splitedData[3];
                City = splitedData[4];
                Country = splitedData[5];

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
