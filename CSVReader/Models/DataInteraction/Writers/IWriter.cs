using CSVReader.Models.DataBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSVReader.Models.DataInteraction.Writers
{
    internal interface IWriter
    {
        void Write(string path, List<Record> records);

        Task WriteAsync(string path, List<Record> records);
    }
}
