using System.Data;
using System.Data.SqlClient;

namespace Bawbee.Infra.Data.SQLRepositories.Dapper
{
    public class DapperConnection : IDapperConnection
    {
        public IDbConnection Connection
        {
            get
            {
                // TODO: inject dependency
                var connectionString = @"Server=.\SQLEXPRESS;Database=Bawbee;MultipleActiveResultSets=true;User Id=sa;Password=123456";
                return new SqlConnection(connectionString);
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
