using CSVReader.DataBase;
using System.Collections.Generic;

namespace CSVReader.DataManagers
{
    internal interface IDataManager
    {
        void Read(string path);
        void Write(string path, List<Record> records);
    }
}
