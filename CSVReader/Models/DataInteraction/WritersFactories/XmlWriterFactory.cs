using CSVReader.Models.DataInteraction.Writers;

namespace CSVReader.Models.DataInteraction.WritersFactories
{
    internal class XmlWriterFactory : WritersFactory
    {
        public override IWriter Create() => new XmlWriter();
    }
}
