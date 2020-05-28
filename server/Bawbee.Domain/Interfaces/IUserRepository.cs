using Bawbee.Domain.Entities;
using System.Threading.Tasks;

namespace Bawbee.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
    }
}
