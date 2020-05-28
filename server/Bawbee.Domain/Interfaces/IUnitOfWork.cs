using System;
using System.Threading.Tasks;

namespace Bawbee.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitTransaction();
    }
}
