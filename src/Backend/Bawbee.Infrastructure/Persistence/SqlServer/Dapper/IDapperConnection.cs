using System;
using System.Data;

namespace Bawbee.Infrastructure.Persistence.SqlServer.Dapper
{
    public interface IDapperConnection : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
