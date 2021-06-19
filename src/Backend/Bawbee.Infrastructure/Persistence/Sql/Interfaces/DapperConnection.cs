using System.Data;
using System.Data.SqlClient;

namespace Bawbee.Infrastructure.Persistence.Sql.Interfaces
{
    public class DapperConnection : IDapperConnection
    {
        private readonly string _connectionString;

        public DapperConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
