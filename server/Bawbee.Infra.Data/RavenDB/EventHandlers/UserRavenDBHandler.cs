using Bawbee.Domain.Entities;
using Bawbee.Domain.Events;
using MediatR;
using Raven.Client.Documents.Session;
using System.Linq;
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
            // TODO: https://stackoverflow.com/questions/13510204/json-net-self-referencing-loop-detected
            //var user = new User(
            //        userRegistered.Name, userRegistered.LastName,
            //        userRegistered.Email, userRegistered.Password, 
            //        userRegistered.BankAccounts, userRegistered.EntryCategories,
            //        userRegistered.UserId);

            var user = new User(@event.Name, @event.LastName, @event.Email, @event.Password, @event.UserId);

            foreach (var ba in @event.BankAccounts)
            {
                var bankAccount = new BankAccount(ba.Name, ba.InitialBalance, ba.UserId);
                user.AddNewBankAccount(bankAccount);
            }

            foreach (var ec in @event.EntryCategories)
            {
                var entryCategory = new EntryCategory(ec.Name, ec.UserId);
                user.AddNewEntryCategory(entryCategory);
            }

            await _session.StoreAsync(user);
            await _session.SaveChangesAsync();
        }
    }
}
