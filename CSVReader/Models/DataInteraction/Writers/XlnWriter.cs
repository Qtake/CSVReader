using CSVReader.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace CSVReader.Models.DataInteraction.Writers
{
    internal class XlnWriter : IWriter
    {
        public async void Write(string path, List<Record> records)
        {
            try
            {
                DataTable dataTable = await Converter.ToDataTableAsync(records);

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

        public async Task WriteAsync(string path, List<Record> records)
        {
            await Task.Run(() => Write(path, records));
        }
    }
}
