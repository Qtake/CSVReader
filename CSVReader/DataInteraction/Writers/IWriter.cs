using CSVReader.DataBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSVReader.DataInteraction.Writers
{
    internal interface IWriter
    {
        void Write(string path, List<Record> records);

        Task WriteAsync(string path, List<Record> records);
    }
}
