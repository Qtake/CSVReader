using CSVReader.DataInteraction.Readers;

namespace CSVReader.DataInteraction.ReadersFactories
{
    internal abstract class ReadersFactory
    {
        public abstract IReader Create();
    }
}
