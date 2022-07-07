using CSVReader.Models.DataInteraction.Readers;

namespace CSVReader.Models.DataInteraction.ReadersFactories
{
    internal class CsvReaderFactory : ReadersFactory
    {
        public override IReader Create() => new CsvReader();
    }
}
