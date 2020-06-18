using System.Data;
using System.Data.SqlClient;

namespace Bawbee.Infra.Data.SQLRepositories.Dapper
{
    public class DapperConnection : IDapperConnection
    {
        private readonly string _connectionString;

        public DapperConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
