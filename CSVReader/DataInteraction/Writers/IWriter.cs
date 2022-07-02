using CSVReader.DataBase;
using System.Collections.Generic;

namespace CSVReader.DataInteraction.Writers
{
    internal interface IWriter
    {
        void Write(string path, List<Record> records);
    }
}
