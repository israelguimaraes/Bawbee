using Bawbee.Domain.Entities;
using Bawbee.Domain.Interfaces;
using Bawbee.Infra.Data.RavenDB;
using Raven.Client.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.ReadRepositories
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly IDocumentStoreHolder _documentStore;

        public UserReadRepository(IDocumentStoreHolder documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task<User> GetByEmail(string email)
        {
            using (var session = _documentStore.Store.OpenAsyncSession())
            {
                return await session.Query<User>().FirstOrDefaultAsync(u => u.Email == email.ToLower());
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using (var session = _documentStore.Store.OpenAsyncSession())
            {
                return await session.Query<User>().ToListAsync();
            }
        }
    }
}
