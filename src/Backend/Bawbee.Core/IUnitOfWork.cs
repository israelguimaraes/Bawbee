using System;
using System.Threading.Tasks;

namespace Bawbee.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitTransaction();
    }
}
