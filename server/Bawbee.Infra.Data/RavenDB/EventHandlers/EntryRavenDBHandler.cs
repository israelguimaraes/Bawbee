using Bawbee.Domain.Entities;
using Bawbee.Domain.Events.Entries;
using MediatR;
using Raven.Client.Documents.Session;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.RavenDB.EventHandlers
{
    public class EntryRavenDBHandler
        : INotificationHandler<EntryAddedEvent>
    {
        private readonly IAsyncDocumentSession _session;

        public EntryRavenDBHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task Handle(EntryAddedEvent @event, CancellationToken cancellationToken)
        {
            var entry = new Entry(
                @event.Description, @event.Value, @event.IsPaid, 
                @event.Observations, @event.DateToPay, @event.UserId, 
                @event.BankAccountId, @event.EntryCategoryId, @event.EntryId);

            await _session.StoreAsync(entry);
            await _session.SaveChangesAsync();
        }
    }
}
