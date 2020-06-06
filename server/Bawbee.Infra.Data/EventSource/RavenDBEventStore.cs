using Bawbee.Domain.Core.Events;
using Raven.Client.Documents.Session;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.EventSource
{
    public class RavenDBEventStore : IEventStore
    {
        private readonly IAsyncDocumentSession _session;

        public RavenDBEventStore(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task Store(Event eventObj)
        {
            if (eventObj == null) return;

            var storeEvent = new StoredEvent(eventObj);

            await _session.StoreAsync(storeEvent);
            await _session.SaveChangesAsync();
        }
    }
}
