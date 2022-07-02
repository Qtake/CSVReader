using CSVReader.DataInteraction.Writers;

namespace CSVReader.DataInteraction.WritersFactories
{
    internal abstract class WritersFactory
    {
        public abstract IWriter Create();
    }
}
