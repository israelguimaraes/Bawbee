using Bawbee.Domain.Entities;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.Data.EF;
using Bawbee.Infra.Data.SQLRepositories.Dapper;
using Dapper;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.SQLRepositories
{
    public class UserSqlServerRepository : IUserRepository
    {
        private readonly IDapperConnection _dapper;
        private readonly BawbeeDbContext _dbContext;

        public UserSqlServerRepository(IDapperConnection dapper, BawbeeDbContext dbContext)
        {
            _dapper = dapper;
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dapper.Connection
                .QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = email });
        }

        public async Task<User> GetByEmailAndPassword(string email, string password)
        {
            const string query = "SELECT * FROM Users WHERE Email = @Email and Password = @Password";

            return await _dapper.Connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email, Password = password });
        }

        public async Task<EntryCategory> GetCategoryByName(string name)
        {
            return await _dapper.Connection
                .QueryFirstOrDefaultAsync<EntryCategory>("SELECT * FROM EntryCategories WHERE Name = @Name", new { Name = name });
        }

        public void AddEntryCategory(EntryCategory category)
        {
            _dbContext.EntryCategories.Add(category);
        }
    }
}
