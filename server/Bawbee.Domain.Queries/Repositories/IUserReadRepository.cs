using Bawbee.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Domain.Queries.Repositories
{
    public interface IUserReadRepository
    {
        Task<User> GetByEmail(string email);
        Task<IEnumerable<User>> GetAll();
    }
}
