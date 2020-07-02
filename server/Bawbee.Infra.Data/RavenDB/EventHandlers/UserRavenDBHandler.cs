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
            await _session.StoreAsync(@event.User);
            await _session.SaveChangesAsync();
        }
    }
}
