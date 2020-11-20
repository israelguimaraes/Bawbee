using Bawbee.Infra.Data.Documents;
using Bawbee.Infra.Data.ReadInterfaces;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.RavenDB.Repositories
{
    public class EntryRavenDBRepository : IEntryReadRepository
    {
        private readonly IAsyncDocumentSession _session;

        public EntryRavenDBRepository(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<EntryDocument>> GetAllByUser(int userId)
        {
            return await _session.Query<EntryDocument>()
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<EntryDocument>> GetAllExpensesByMonth(int userId, int month)
        {
            return await _session.Query<EntryDocument>()
                .Where(e => e.UserId == userId &&
                       e.DateToPay.Month == month &&
                       e.Value < 0)
                .ToListAsync();
        }
    }
}
