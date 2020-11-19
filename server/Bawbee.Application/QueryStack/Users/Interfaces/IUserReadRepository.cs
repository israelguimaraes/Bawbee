using Bawbee.Application.QueryStack.Users.Documents;
using Bawbee.Domain.AggregatesModel.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Application.QueryStack.Users.Interfaces
{
    public interface IUserReadRepository
    {
        Task<User> GetByEmail(string email);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmailAndPassword(string email, string password);
        Task<IEnumerable<EntryCategoryDocument>> GetCategoriesByUser(int userId);
        Task<IEnumerable<BankAccountDocument>> GetBankAccountsByUser(int userId);
        Task<UserDocument> GetByUserId(int userId);
    }
}
