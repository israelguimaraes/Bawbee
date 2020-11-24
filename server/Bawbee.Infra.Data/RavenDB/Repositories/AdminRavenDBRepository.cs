using Bawbee.Infra.Data.ReadInterfaces;
using Raven.Client.Documents.Operations;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.RavenDB.Repositories
{
    public class AdminRavenDBRepository : IAdminRavenDBRepository
    {
        private readonly IDocumentStoreHolder _documentStore;

        public AdminRavenDBRepository(IDocumentStoreHolder documentStore)
        {
            _documentStore = documentStore;
        }

        public Task DeleteAllDocuments()
        {
            var query = @"from @all_docs";
            return _documentStore.Store.Operations.SendAsync(new DeleteByQueryOperation(query));
        }
    }
}
