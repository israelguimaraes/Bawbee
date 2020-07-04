using Bawbee.Application.Query.Users.Documents;
using Bawbee.Domain.Events;
using MediatR;
using Raven.Client.Documents.Session;
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

            foreach (var b in @event.User.BankAccounts)
            {
                userDocument.BankAccounts.Add(new BankAccountDocument
                {
                    BankAccountId = b.Id,
                    InitialBalance = b.InitialBalance,
                    Name = b.Name
                });
            }

            foreach (var ec in @event.User.EntryCategories)
            {
                userDocument.EntryCategories.Add(new EntryCategoryDocument
                {
                    EntryCategoryId = ec.Id,
                    Name = ec.Name
                });
            }

            await _session.StoreAsync(userDocument);
            await _session.SaveChangesAsync();
        }
    }
}