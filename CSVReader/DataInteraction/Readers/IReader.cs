using System.Threading.Tasks;

namespace CSVReader.DataInteraction.Readers
{
    internal interface IReader
    {
        void Read(string path);

        Task ReadAsync(string path);
    }
}
