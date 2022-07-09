using CSVReader.Models.DataInteraction.Writers;

namespace CSVReader.Models.DataInteraction.WritersFactories
{
    internal class XlnWriterFactory : WritersFactory
    {
        public override IWriter Create() => new XlnWriter();
    }
}
