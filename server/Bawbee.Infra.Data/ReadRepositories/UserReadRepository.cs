using Bawbee.Domain.Queries.Repositories;
using Bawbee.Domain.Queries.Users;
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

        public async Task<UserDocument> GetByEmail(string email)
        {
            using (var session = _documentStore.Store.OpenAsyncSession())
            {
                return await session.Query<UserDocument>().FirstOrDefaultAsync(u => u.Email == email);
            }
        }

        public async Task<IEnumerable<UserDocument>> GetAll()
        {
            using (var session = _documentStore.Store.OpenAsyncSession())
            {
                return await session.Query<UserDocument>().ToListAsync();
            }
        }
    }
}
