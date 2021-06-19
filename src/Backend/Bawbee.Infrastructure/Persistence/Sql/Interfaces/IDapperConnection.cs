using System;
using System.Data;

namespace Bawbee.Infrastructure.Persistence.Sql.Interfaces
{
    public interface IDapperConnection : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
