using Bawbee.Domain.Entities;
using Bawbee.Domain.Events.Entries;
using MediatR;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
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
            //var user = await _session.Query<User>()
            //    .Where(u => u.UserId == @event.UserId)
            //    .FirstOrDefaultAsync();

            //var bankAccount = user.GetBankAccountById(@event.BankAccountId);
            //var entryCategory = user.GetEntryCategoryById(@event.EntryCategoryId);

            //var entry = new Entry(
            //    @event.Description, @event.Value, @event.IsPaid,
            //    @event.Observations, @event.DateToPay, @event.UserId,
            //    @event.BankAccountId, @event.EntryCategoryId,
            //    bankAccount, entryCategory, @event.UserId);

            //await _session.StoreAsync(entry);
            //await _session.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
