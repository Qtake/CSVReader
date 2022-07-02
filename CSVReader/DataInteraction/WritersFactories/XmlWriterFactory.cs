using CSVReader.DataInteraction.Writers;

namespace CSVReader.DataInteraction.WritersFactories
{
    internal class XmlWriterFactory : WritersFactory
    {
        public override IWriter Create() =>  new XmlWriter();
    }
}
