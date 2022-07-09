using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace CSVReader.Models.DataBase
{
    internal class Converter
    {
        public static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in properties)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[properties.Length];

                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public async static Task<DataTable> ToDataTableAsync<T>(IEnumerable<T> items)
        {
            return await Task.Run(() => ToDataTable(items));
        }
    }
}
