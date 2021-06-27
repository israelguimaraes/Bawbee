using System;
using System.Data;

namespace Bawbee.Infrastructure.Persistence.SqlServer.EFCore.Dapper
{
    public interface IDapperConnection : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
