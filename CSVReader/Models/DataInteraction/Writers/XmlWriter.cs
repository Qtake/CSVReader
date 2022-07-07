using CSVReader.Models.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CSVReader.Models.DataInteraction.Writers
{
    internal class XmlWriter : IWriter
    {
        public void Write(string path, List<Record> records)
        {
            try
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<Record>));
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    xmlFormatter.Serialize(writer, records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task WriteAsync(string path, List<Record> records)
        {
            await Task.Run(() => Write(path, records));
        }
    }
}
