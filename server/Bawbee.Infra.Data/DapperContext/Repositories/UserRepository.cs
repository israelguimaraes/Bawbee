using Bawbee.Domain.Entities;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.Data.DapperContext.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.DapperContext.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapper _dapper;

        public UserRepository(IDapper dapper)
        {
            _dapper = dapper;
        }

        public async Task Add(User user)
        {
            await _dapper.Connection.InsertAsync(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            _dapper.Connection.Open();

            // TODO: change to RavenDB
            return await _dapper.Connection.QueryFirstOrDefaultAsync<User>($"SELECT * FROM Users WHERE Email = '{email}'");
        }
    }
}
