using CSVReader.DataBase;
using CSVReader.DataBase.Repositories;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;

namespace CSVReader.DataInteraction.Readers
{
    internal class CsvReader : IReader
    {
        private IRepository<Record>? _repository;

        public CsvReader()
        {
            _repository = null;
        }

        public async void Read(string path)
        {
            Record record = new Record();
            _repository = new MsSqlRepository(ApplicationSettings.Default.DatabaseConnectionString);
            using (MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateFromFile(path))
            using (MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream())
            using (StreamReader reader = new StreamReader(memoryMappedViewStream))
            {
                string? line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (record.ParseRecord(line))
                    {
                        _repository.Add(record);
                        _repository.Save();
                    }
                }
            }
            _repository.Dispose();
        }

        public async Task ReadAsync(string path)
        {
            await Task.Run(() => Read(path));
        }
    }
}
