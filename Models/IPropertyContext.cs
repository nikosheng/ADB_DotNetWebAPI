using Oracle.ManagedDataAccess.Client;

namespace WebAPIProjectNet22.Models
{
    public interface IPropertyContext
    {
        OracleCommand GetCommand();
        OracleConnection GetConnection();
    }
}
