using System;
using System.Data;

namespace Bawbee.Infra.Data.SQLRepositories.Dapper
{
    public interface IDapperConnection : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
