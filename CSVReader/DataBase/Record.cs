using System;

namespace CSVReader.DataBase
{
    internal class Record
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Record()
        {
            Date = DateTime.Now;
            Name = string.Empty;
            Surname = string.Empty;
            Patronymic = string.Empty;
            City = string.Empty;
            Country = string.Empty;
        }

        public Record(DateTime date, string name, string surname, string patronymic, string city, string country)
        {
            Date = date;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            City = city;
            Country = country;
        }   

        public bool ParseRecord(string fileLine)
        {
            try
            {
                Id = 0;
                string[] splitedData = fileLine.Split(';');
                Date = Convert.ToDateTime(splitedData[0]);
                Name = splitedData[1];
                Surname = splitedData[2];
                Patronymic = splitedData[3];
                City = splitedData[4];
                Country = splitedData[5];
                
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }   
        }
    }
}
