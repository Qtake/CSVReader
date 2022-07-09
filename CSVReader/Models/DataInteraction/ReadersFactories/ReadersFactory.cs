using CSVReader.Models.DataInteraction.Readers;

namespace CSVReader.Models.DataInteraction.ReadersFactories
{
    internal abstract class ReadersFactory
    {
        public abstract IReader Create();
    }
}
