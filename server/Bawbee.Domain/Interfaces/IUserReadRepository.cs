using Bawbee.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Domain.Interfaces
{
    public interface IUserReadRepository
    {
        Task<User> GetByEmail(string email);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmailAndPassword(string email, string password);
    }
}
