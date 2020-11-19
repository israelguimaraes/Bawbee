using Bawbee.Domain.AggregatesModel.Users;
using System.Threading.Tasks;

namespace Bawbee.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetByEmailAndPassword(string email, string password);
        Task<Category> GetCategoryByName(string name, int userId);
        Task AddEntryCategory(Category category);
        Task<BankAccount> GetBankAccountByName(string name, int userId);
        Task AddBankAccount(BankAccount bankAccount);
    }
}
