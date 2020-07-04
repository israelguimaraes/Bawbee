using Bawbee.Application.Query.Users.Documents;
using Bawbee.Domain.Events;
using MediatR;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.RavenDB.EventHandlers
{
    public class UserRavenDBHandler
        : INotificationHandler<UserRegisteredEvent>
    {
        private readonly IAsyncDocumentSession _session;

        public UserRavenDBHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken)
        {
            // TODO: automapper?

            var userDocument = new UserDocument();

            userDocument.UserId = @event.User.Id;
            userDocument.Name = @event.User.Name;
            userDocument.LastName = @event.User.LastName;
            userDocument.Email = @event.User.Email;
            userDocument.Password = @event.User.Password;

            var bankAccountsDocument = new List<BankAccountDocument>(@event.User.BankAccounts.Count);
            foreach (var b in @event.User.BankAccounts)
            {
                bankAccountsDocument.Add(new BankAccountDocument
                {
                    BankAccountId = b.Id,
                    InitialBalance = b.InitialBalance,
                    Name = b.Name
                });
            }

            var categories = new List<EntryCategoryDocument>(@event.User.EntryCategories.Count);
            foreach (var b in @event.User.EntryCategories)
            {
                categories.Add(new EntryCategoryDocument
                {
                    EntryCategoryId = b.Id,
                    Name = b.Name
                });
            }

            await _session.StoreAsync(userDocument);
            await _session.SaveChangesAsync();
        }
    }
}