using CSVReader.DataBase;
using CSVReader.DataBase.Repositories;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;

namespace CSVReader.DataInteraction.Readers
{
    internal class CsvReader : IReader
    {
        public async void Read(string path)
        {
            Record record = new Record();
            MsSqlRepository repository = new MsSqlRepository(ApplicationSettings.Default.DatabaseConnectionString);
            using (MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateFromFile(path))
            using (MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream())
            using (StreamReader reader = new StreamReader(memoryMappedViewStream))
            {
                string? line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (record.ParseRecord(line))
                    {
                        repository.Add(record);
                        repository.Save();
                    }
                }
            }
            repository.Dispose();
        }

        public async Task ReadAsync(string path)
        {
            await Task.Run(() => Read(path));
        }
    }
}
