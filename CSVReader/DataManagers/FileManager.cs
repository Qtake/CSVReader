using CSVReader.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CSVReader.DataManagers
{
    internal class FileManager : IDataManager
    {
        public void Read(string path)
        {
            Record record = new Record();
            using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bufferedStream = new BufferedStream(fileStream))
            using (StreamReader reader = new StreamReader(bufferedStream))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (record.ParseRecord(line))
                    {
                        using (ApplicationContext db = new ApplicationContext())
                        {
                            db.Records.Add(record);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public void Write(string path, List<Record> records)
        {
            string extension = Path.GetExtension(path);

            switch (extension)
            {
                case ".xml":
                    SaveAsXML(path);
                    break;
                case ".xls":
                    SaveAsXLS(path);
                    break ;
            }
        }

        private void SaveAsXML(string path)
        {
            try
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<Record>));
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        xmlFormatter.Serialize(writer, db.Records.ToList());
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SaveAsXLS(string path)
        {

        }
    }
}
