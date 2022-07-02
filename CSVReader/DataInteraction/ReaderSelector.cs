using CSVReader.DataInteraction.Readers;
using CSVReader.DataInteraction.ReadersFactories;
using System.IO;

namespace CSVReader.DataManagers
{
    internal class ReaderSelector
    {
        public static IReader Select(string path)
        {
            string extension = Path.GetExtension(path);
            ReadersFactory factory = null!;

            switch (extension)
            {
                case ".csv":
                    factory = new CsvReaderFactory();
                    break;
            }

            return factory.Create();
        }
    }
}
