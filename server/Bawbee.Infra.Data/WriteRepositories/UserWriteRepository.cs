using Bawbee.Domain.Entities;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.Data.WriteRepositories.Dapper;
using Dapper.Contrib.Extensions;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.WriteRepositories
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly IDapperConnection _dapper;

        public UserWriteRepository(IDapperConnection dapper)
        {
            _dapper = dapper;
        }

        public async Task Add(User user)
        {
            await _dapper.Connection.InsertAsync(user);
        }
    }
}
