using CSVReader.DataInteraction.Writers;
using CSVReader.DataInteraction.WritersFactories;
using System.IO;

namespace CSVReader.DataManagers
{
    internal class WriterSelector
    {
        public static IWriter Select(string path)
        {
            string extension = Path.GetExtension(path);
            WritersFactory factory = null!;

            switch (extension)
            {
                case ".xml":
                    factory = new XmlWriterFactory();
                    break;
                case ".xls":
                    factory = new XlnWriterFactory();
                    break;
            }

            return factory.Create();
        }
    }
}
