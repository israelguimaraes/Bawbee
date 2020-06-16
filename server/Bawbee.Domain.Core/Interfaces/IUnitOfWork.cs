using System;
using System.Threading.Tasks;

namespace Bawbee.Domain.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitTransaction();
    }
}
