using Bawbee.Domain.Interfaces;
using Bawbee.Infra.Data.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.EntityFramework.Repositories
{
    public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : class
    {
        public BawbeeDbContext Context { get; private set; }
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(BawbeeDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task Delete(TId id)
        {
            var entity = await GetById(id);
            DbSet.Remove(entity);
        }

        public async Task<TEntity> GetById(TId id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
