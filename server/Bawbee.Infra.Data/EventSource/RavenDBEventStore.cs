using Bawbee.Domain.Core.Events;
using Bawbee.Infra.Data.RavenDB;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.EventSource
{
    public class RavenDBEventStore : IEventStore
    {
        private readonly IDocumentStoreHolder _documentStore;

        public RavenDBEventStore(IDocumentStoreHolder documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task Store(Event eventObj)
        {
            if (eventObj == null) return;

            var storeEvent = new StoredEvent(eventObj);

            using (var session = _documentStore.Store.OpenAsyncSession())
            {
                await session.StoreAsync(storeEvent);
                await session.SaveChangesAsync();
            }
        }
    }
}
