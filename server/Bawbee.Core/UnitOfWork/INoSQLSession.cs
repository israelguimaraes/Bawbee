using System;
using System.Threading.Tasks;

namespace Bawbee.Core.UnitOfWork
{
    public interface INoSQLUnitOfWork
    {
        Task CommitTransaction();
    }
}
