using System;

namespace Bawbee.Domain.Interfaces
{
    public interface IBaseRepository<T, TId> : IDisposable
    {
        void Add(T entity);
        T GetById(TId id);
        void Update(T entity);
        void Delete(TId id);
        void SaveChanges();
    }
}
