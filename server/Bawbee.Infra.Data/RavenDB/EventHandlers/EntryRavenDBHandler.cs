using Bawbee.Application.Query.Users.Documents;
using Bawbee.Domain.Events.Entries;
using MediatR;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System.Linq;
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
            var user = await _session.Query<UserDocument>()
                .Where(u => u.UserId == @event.UserId)
                .FirstOrDefaultAsync();

            var entryDocument = new EntryDocument();
            entryDocument.UserDocumentId = user.Id;
            entryDocument.Description = @event.Description;
            entryDocument.Value = @event.Value;
            entryDocument.IsPaid = @event.IsPaid;
            entryDocument.Observations = @event.Observations;
            entryDocument.DateToPay = @event.DateToPay;

            entryDocument.BankAccountId = @event.BankAccountId;
            entryDocument.BankAccountName = user.BankAccounts.FirstOrDefault(b => b.BankAccountId == @event.BankAccountId).Name;

            entryDocument.EntryCategoryId = @event.EntryCategoryId;
            entryDocument.EntryCategoryName = user.EntryCategories.FirstOrDefault(e => e.EntryCategoryId == @event.EntryCategoryId).Name;

            await _session.StoreAsync(entryDocument);
            await _session.SaveChangesAsync();
        }
    }
}
