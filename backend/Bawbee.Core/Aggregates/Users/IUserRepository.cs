using Bawbee.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace Bawbee.Core.Aggregates.Users
{
    public interface IUserRepository : IAggregateRepository<User>
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetByEmailAndPassword(string email, string password);
        Task<Category> GetCategoryByName(string name, int userId);
        Task CreateCategory(Category category);
        Task<BankAccount> GetBankAccountByName(string name, int userId);
        Task CreateBankAccount(BankAccount bankAccount);
    }
}
