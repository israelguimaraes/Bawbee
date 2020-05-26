using System;
using System.Threading.Tasks;

namespace Bawbee.Domain.Interfaces
{
    public interface IBaseRepository<T, TId> : IDisposable
    {
        Task Add(T entity);
        Task<T> GetById(TId id);
        Task Update(T entity);
        Task Delete(TId id);
        Task SaveChanges();
    }
}
