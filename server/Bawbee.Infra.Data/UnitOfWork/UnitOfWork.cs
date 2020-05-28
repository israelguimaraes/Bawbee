using Bawbee.Domain.Interfaces;
using Bawbee.Infra.Data.EntityFramework.Contexts;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BawbeeDbContext _context;

        public UnitOfWork(BawbeeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitTransaction()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
