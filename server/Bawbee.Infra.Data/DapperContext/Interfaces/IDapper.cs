using System;
using System.Data;

namespace Bawbee.Infra.Data.DapperContext.Interfaces
{
    public interface IDapper : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
