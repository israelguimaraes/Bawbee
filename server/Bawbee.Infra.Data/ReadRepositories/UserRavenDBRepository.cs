using Bawbee.Application.Query.Users.Interfaces;
using Bawbee.Domain.Entities;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.ReadRepositories
{
    public class UserRavenDBRepository : IUserReadRepository
    {
        private readonly IAsyncDocumentSession _session;

        public UserRavenDBRepository(IAsyncDocumentSession session)
        {
            _session = session;
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
    }
}
