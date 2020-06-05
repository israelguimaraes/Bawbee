using System;
using System.Data;

namespace Bawbee.Infra.Data.WriteRepositories.Dapper
{
    public interface IDapperConnection : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
