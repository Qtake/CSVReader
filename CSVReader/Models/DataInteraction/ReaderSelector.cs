using CSVReader.Models.DataInteraction.Readers;
using CSVReader.Models.DataInteraction.ReadersFactories;
using System.IO;

namespace CSVReader.Models.DataInteraction
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
