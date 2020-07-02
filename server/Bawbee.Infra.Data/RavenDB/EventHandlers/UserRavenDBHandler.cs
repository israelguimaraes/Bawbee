using Bawbee.Domain.Entities;
using Bawbee.Domain.Events;
using MediatR;
using Raven.Client.Documents;
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

        //public async Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken)
        //{
        //    var user = new User(
        //            @event.Name, @event.LastName,
        //            @event.Email, @event.Password,
        //            @event.BankAccounts, @event.EntryCategories,
        //            @event.UserId);

        //    await _session.StoreAsync(user);
        //    await _session.SaveChangesAsync();
        //}

        public async Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken)
        {
            var user = await _session.Query<User>().FirstOrDefaultAsync(u => u.UserId == @event.User.UserId);

            user ??= new User(@event.User.Name, @event.User.LastName, @event.User.Email, @event.User.Password, @event.User.UserId);

            await _session.StoreAsync(user);
            await _session.SaveChangesAsync();
        }
    }
}
