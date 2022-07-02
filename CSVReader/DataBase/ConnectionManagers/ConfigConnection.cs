using System.Configuration;

namespace CSVReader.DataBase.ConnectionManagers
{
    internal class ConfigConnection : IConnectionManager
    {
        private readonly string _connectionName;

        public ConfigConnection(string connectionName)
        {
            _connectionName = connectionName;
        }

        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[_connectionName].ConnectionString;
        }
    }
}
