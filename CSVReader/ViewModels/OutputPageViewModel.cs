using CSVReader.Models.DataBase;
using CSVReader.Models.DataBase.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CSVReader.ViewModels
{
    internal class OutputPageViewModel
    {
        public List<Record> DataBaseRecords { get; private set; }

        private readonly IRepository<Record> _repository;

        public OutputPageViewModel()
        {
            DataBaseRecords = null!;
            _repository = new MsSqlRepository(ApplicationSettings.Default.DatabaseConnectionString);

            GetRecords();
        }

        private async void GetRecords()
        {
            using (_repository)
            {
                IQueryable<Record> records = _repository.SelectAll();
                DataBaseRecords = await records.ToListAsync();
            }
        }
    }
}
