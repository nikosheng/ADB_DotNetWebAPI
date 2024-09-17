using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace WebAPIProjectNet22.Models
{

    public class PropertyContext : IPropertyContext
    {
        private IConfiguration _config;
        private OracleConnection _connection;
        private OracleCommand _cmd;
        private string _connectionString;

        public PropertyContext(IConfiguration configuration)
        {
            _config = configuration;
            _connectionString = _config.GetConnectionString("DBConn");
            //Enter directory where you unzipped your cloud credentials
            OracleConfiguration.TnsAdmin = @"C:\Users\opc\source\repos\wallet";
            OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;
        }

        public OracleConnection GetConnection()
        {
            _connection = new OracleConnection(_connectionString);
            return _connection;
        }

        public OracleCommand GetCommand()
        {
            _cmd = _connection.CreateCommand();
            return _cmd;
        }
    }
}
