using System;
using System.Threading.Tasks;

namespace Bawbee.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitTransaction();
    }
}
