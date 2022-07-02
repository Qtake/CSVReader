using CSVReader.DataInteraction.Readers;

namespace CSVReader.DataInteraction.ReadersFactories
{
    internal class CsvReaderFactory : ReadersFactory
    {
        public override IReader Create() => new CsvReader();
    }
}
