using Bawbee.Domain.Entities;
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

        public async Task Handle(UserRegisteredEvent userRegistered, CancellationToken cancellationToken)
        {
            var user = new User(
                    userRegistered.Name, userRegistered.LastName,
                    userRegistered.Email, userRegistered.Password,
                    userRegistered.UserId);

            await _session.StoreAsync(user);
            await _session.SaveChangesAsync();
        }
    }
}
