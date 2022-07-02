using CSVReader.DataInteraction.Writers;

namespace CSVReader.DataInteraction.WritersFactories
{
    internal class XlnWriterFactory : WritersFactory
    {
        public override IWriter Create() => new XlnWriter();
    }
}
