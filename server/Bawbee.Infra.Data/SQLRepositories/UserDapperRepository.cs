using Bawbee.Domain.Entities;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.Data.SQLRepositories.Dapper;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.SQLRepositories
{
    public class UserSqlServerRepository : IUserRepository
    {
        private readonly IDapperConnection _dapper;

        public UserSqlServerRepository(IDapperConnection dapper)
        {
            _dapper = dapper;
        }

        public async Task Add(User user)
        {
            await _dapper.Connection.InsertAsync(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dapper.Connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = email });
        }

        public async Task<User> GetByEmailAndPassword(string email, string password)
        {
            const string query = "SELECT * FROM Users WHERE Email = @Email and Password = @Password";

            return await _dapper.Connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email, Password = password });
        }
    }
}
