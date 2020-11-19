using Bawbee.Application.Query.Users.Documents;
using Bawbee.Application.Query.Users.Interfaces;
using Bawbee.Domain.AggregatesModel.Users;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.NoSQLRepositories
{
    public class UserRavenDBRepository : IUserReadRepository
    {
        private readonly IAsyncDocumentSession _session;

        public UserRavenDBRepository(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task<UserDocument> GetByUserId(int userId)
        {
            return await _session.Query<UserDocument>().FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _session.Query<User>().FirstOrDefaultAsync(u => u.Email == email.ToLower());
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _session.Query<User>().ToListAsync();
        }

        public async Task<User> GetByEmailAndPassword(string email, string password)
        {
            return await _session.Query<User>().FirstOrDefaultAsync(u => u.Email == email.ToLower() && u.Password == password);
        }

        public async Task<IEnumerable<EntryCategoryDocument>> GetCategoriesByUser(int userId)
        {
            return await _session.Query<UserDocument>()
                .Where(u => u.UserId == userId)
                .Select(u => u.EntryCategories)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BankAccountDocument>> GetBankAccountsByUser(int userId)
        {
            return await _session.Query<UserDocument>()
                .Where(u => u.UserId == userId)
                .Select(u => u.BankAccounts)
                .FirstOrDefaultAsync();
        }
    }
}
