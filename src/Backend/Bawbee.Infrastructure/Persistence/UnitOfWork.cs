using Bawbee.Core;
using Bawbee.Infrastructure.Persistence.SqlServer.EFCore.Contexts;
using System;
using System.Threading.Tasks;

namespace Bawbee.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BawbeeDbContext _dbContext;

        public UnitOfWork(BawbeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CommitTransaction()
        {
            try
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // TODO: log
                return false;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
