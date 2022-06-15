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

        //public Record()
        //{
        //    Date = DateTime.Now;
        //    Name = string.Empty;
        //    Surname = string.Empty;
        //    Patronymic = string.Empty;
        //    City = string.Empty;
        //    Country = string.Empty;
        //}

        public Record(DateTime date, string name, string surname, string patronymic, string city, string country)
        {
            Date = date;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            City = city;
            Country = country;
        }   
    }
}
