using Bawbee.Domain.Entities;
using System.Threading.Tasks;

namespace Bawbee.Domain.Interfaces
{
    public interface IUserRepository //: IBaseRepository<User, int>
    {
        Task<User> GetByEmail(string email);
        Task Add(User user);
    }
}
