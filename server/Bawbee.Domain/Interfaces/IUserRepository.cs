using Bawbee.Domain.Entities;
using System.Threading.Tasks;

namespace Bawbee.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetByEmailAndPassword(string email, string password);
        Task<EntryCategory> GetCategoryByName(string name);
        void AddEntryCategory(EntryCategory category);
    }
}
