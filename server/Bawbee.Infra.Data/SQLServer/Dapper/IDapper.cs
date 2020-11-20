using System;
using System.Data;

namespace Bawbee.Infra.Data.SQLServer.Dapper
{
    public interface IDapperConnection : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
