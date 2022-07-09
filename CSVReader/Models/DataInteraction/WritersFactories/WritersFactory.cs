using CSVReader.Models.DataInteraction.Writers;

namespace CSVReader.Models.DataInteraction.WritersFactories
{
    internal abstract class WritersFactory
    {
        public abstract IWriter Create();
    }
}
