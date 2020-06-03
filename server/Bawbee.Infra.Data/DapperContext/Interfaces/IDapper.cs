using System;
using System.Data;

namespace Bawbee.Infra.Data.DapperContext.Interfaces
{
    public interface IDapperConnection : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
