using Bawbee.Application.Query.Users.Documents;
using Bawbee.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Application.Query.Users.Interfaces
{
    public interface IUserReadRepository
    {
        Task<User> GetByEmail(string email);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmailAndPassword(string email, string password);
        Task<IEnumerable<EntryCategoryDocument>> GetCategoriesByUser(int userId);
        Task<IEnumerable<BankAccountDocument>> GetBankAccountsByUser(int userId);
        Task<UserDocument> GetById(int userId);
    }
}
