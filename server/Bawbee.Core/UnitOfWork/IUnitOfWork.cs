using System;
using System.Threading.Tasks;

namespace Bawbee.Domain.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitTransaction();
    }
}
