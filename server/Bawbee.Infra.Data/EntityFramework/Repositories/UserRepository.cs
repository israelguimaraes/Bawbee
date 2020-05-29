using Bawbee.Domain.Entities;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.Data.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.EntityFramework.Repositories
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(BawbeeDbContext context)
            : base(context)
        {

        }

        public Task<User> GetByEmail(string email)
        {
            var dbConn = Context.Database.GetDbConnection();
            var str = dbConn.ConnectionString;


            return DbSet.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
