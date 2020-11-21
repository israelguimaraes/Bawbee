using Bawbee.Core.UnitOfWork;
using Raven.Client.Documents.Session;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.Uow
{
    public class NoSQLUnitOfWork : INoSQLUnitOfWork
    {
        private readonly IAsyncDocumentSession _session;

        public NoSQLUnitOfWork(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task CommitTransaction()
        {
            await _session.SaveChangesAsync();
        }
    }
}
