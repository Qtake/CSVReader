using CSVReader.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;

namespace CSVReader.DataManagers
{
    internal class FileManager : IDataManager
    {
        public void Read(string path)
        {
            Record record = new Record();
            ApplicationContext context = new ApplicationContext();
            using (MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateFromFile(path))
            using (MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream())
            using (StreamReader reader = new StreamReader(memoryMappedViewStream))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (record.ParseRecord(line))
                    {
                        context.Records.Add(record);
                        context.SaveChanges();
                    }
                }
            }
            context.Dispose();
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
                    SaveAsXLS(path, records);
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

        private void SaveAsXLS(string path, List<Record> records)
        {
            try
            {
                DataTable dataTable = Converter.ToDataTable(records);

                if (dataTable == null || dataTable.Columns.Count == 0)
                {
                    throw new Exception("Null or empty input table.\n");
                }

                Excel.Application excelApplication = new Excel.Application();
                excelApplication.Workbooks.Add();
                Excel._Worksheet workSheet = excelApplication.ActiveSheet;

                for (var i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (var j = 0; j < dataTable.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 1, j + 1] = dataTable.Rows[i][j];
                    }
                }

                workSheet.SaveAs(path);
                excelApplication.Quit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
