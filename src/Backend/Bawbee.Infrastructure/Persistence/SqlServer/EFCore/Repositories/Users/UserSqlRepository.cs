using Bawbee.Core.Aggregates.Users;
using Bawbee.Infrastructure.Persistence.SqlServer.EFCore.Contexts;
using Bawbee.Infrastructure.Persistence.SqlServer.EFCore.Dapper;
using Dapper;
using System.Threading.Tasks;

namespace Bawbee.Infrastructure.Persistence.SqlServer.EFCore.Repositories.Users
{
    public class UserSqlRepository : IUserRepository
    {
        private readonly IDapperConnection _dapper;
        private readonly BawbeeDbContext _dbContext;

        public UserSqlRepository(IDapperConnection dapper, BawbeeDbContext dbContext)
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

        public async Task<Category> GetCategoryByName(string name, int userId)
        {
            var query = "SELECT * FROM EntryCategories WHERE Name = @Name AND UserId = @UserId";

            return await _dapper.Connection.QueryFirstOrDefaultAsync<Category>(query, new { Name = name, UserId = userId });
        }

        public async Task CreateCategory(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public async Task<BankAccount> GetBankAccountByName(string name, int userId)
        {
            var query = "SELECT * FROM BankAccounts WHERE Name = @Name AND UserId = @UserId";

            return await _dapper.Connection.QueryFirstOrDefaultAsync<BankAccount>(query, new { Name = name, UserId = userId });
        }

        public async Task CreateBankAccount(BankAccount bankAccount)
        {
            await _dbContext.BankAccounts.AddAsync(bankAccount);
        }
    }
}
