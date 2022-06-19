using CSVReader.DataBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Xml.Serialization;

namespace CSVReader.DataManagers
{
    internal class FileManager : IDataManager
    {
        public void Read(string path)
        {
            Record record = new Record();
            ApplicationContext db = new ApplicationContext();
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(path))
            using (MemoryMappedViewStream mmvs = mmf.CreateViewStream())
            using (StreamReader sr = new StreamReader(mmvs))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (record.ParseRecord(line))
                    {
                        db.Records.Add(record);
                        db.SaveChanges();
                    }
                }
            }
            db.Dispose();
        }

        public void Write(string path, List<Record> records)
        {
            string extension = Path.GetExtension(path);

            switch (extension)
            {
                case ".xml":
                    SaveAsXML(path, records);
                    break;
                case ".xls":
                    SaveAsXLS(path);
                    break ;
            }
        }

        private void SaveAsXML(string path, List<Record> records)
        {
            try
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<Record>));
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    xmlFormatter.Serialize(writer, records);
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
