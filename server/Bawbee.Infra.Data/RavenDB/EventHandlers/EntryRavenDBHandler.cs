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
    public class EntryRavenDBHandler : 
        INotificationHandler<EntryCreatedEvent>, 
        INotificationHandler<EntryUpdatedEvent>,
        INotificationHandler<EntryDeletedEvent>
    {
        private readonly IAsyncDocumentSession _session;

        public EntryRavenDBHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task Handle(EntryCreatedEvent @event, CancellationToken cancellationToken)
        {
            var user = await _session.Query<UserDocument>()
                .Where(u => u.UserId == @event.UserId)
                .FirstOrDefaultAsync();

            var entryDocument = new EntryDocument();
            entryDocument.UserDocumentId = user.Id;
            entryDocument.EntryId = @event.EntryId;
            entryDocument.Description = @event.Description;
            entryDocument.Value = @event.Value;
            entryDocument.IsPaid = @event.IsPaid;
            entryDocument.Observations = @event.Observations;
            entryDocument.DateToPay = @event.DateToPay;

            entryDocument.UserId = @event.UserId;

            entryDocument.BankAccountId = @event.BankAccountId;
            entryDocument.BankAccountName = user.BankAccounts.FirstOrDefault(b => b.BankAccountId == @event.BankAccountId).Name;

            entryDocument.EntryCategoryId = @event.CategoryId;
            entryDocument.EntryCategoryName = user.EntryCategories.FirstOrDefault(e => e.EntryCategoryId == @event.CategoryId).Name;

            await _session.StoreAsync(entryDocument);
            await _session.SaveChangesAsync();
        }

        public async Task Handle(EntryUpdatedEvent @event, CancellationToken cancellationToken)
        {
            var entryDocument = await _session.Query<EntryDocument>().FirstOrDefaultAsync(e => e.EntryId == @event.EntryId);

            entryDocument.Description = @event.Description;
            entryDocument.Value = @event.Value;
            entryDocument.Observations = @event.Observations;
            entryDocument.DateToPay = @event.DateToPay;

            var isBankAccountChanged = entryDocument.BankAccountId != @event.BankAccountId;
            var isCategoryChanged = entryDocument.EntryCategoryId != @event.CategoryId;

            if (isBankAccountChanged || isCategoryChanged)
            {
                var user = await _session.LoadAsync<UserDocument>(entryDocument.UserDocumentId);

                if (isBankAccountChanged)
                {
                    entryDocument.BankAccountId = @event.BankAccountId;
                    entryDocument.BankAccountName = user.BankAccounts.FirstOrDefault(b => b.BankAccountId == @event.BankAccountId).Name;
                }

                if (isCategoryChanged)
                {
                    entryDocument.EntryCategoryId = @event.CategoryId;
                    entryDocument.EntryCategoryName = user.EntryCategories.FirstOrDefault(e => e.EntryCategoryId == @event.CategoryId).Name;
                }
            }

            await _session.SaveChangesAsync();
        }

        public async Task Handle(EntryDeletedEvent @event, CancellationToken cancellationToken)
        {
            var entryDocument = await _session.Query<EntryDocument>().FirstOrDefaultAsync(e => e.EntryId == @event.EntryId);

            _session.Delete(entryDocument);
            await _session.SaveChangesAsync();
        }
    }
}
