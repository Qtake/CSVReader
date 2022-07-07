using System.Threading.Tasks;

namespace CSVReader.Models.DataInteraction.Readers
{
    internal interface IReader
    {
        void Read(string path);

        Task ReadAsync(string path);
    }
}
