using Bawbee.Domain.AggregatesModel.Users;
using Bawbee.Infra.Data.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.ReadInterfaces
{
    public interface IUserReadRepository
    {
        Task<User> GetByEmail(string email);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmailAndPassword(string email, string password);
        Task<IEnumerable<Documents.Category>> GetCategoriesByUser(int userId);
        Task<IEnumerable<Documents.Users.BankAccount>> GetBankAccountsByUser(int userId);
        Task<UserDocument> GetByUserId(int userId);
    }
}
