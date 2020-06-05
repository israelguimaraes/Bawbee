using Bawbee.Domain.Entities;
using Bawbee.Domain.Events.Users;
using Bawbee.Domain.Queries.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.RavenDB.EventHandlers
{
    public class UserRavenDBHandler
        : INotificationHandler<UserRegisteredEvent>
    {
        private readonly IDocumentStoreHolder _documentStore;

        public UserRavenDBHandler(IDocumentStoreHolder documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task Handle(UserRegisteredEvent userRegistered, CancellationToken cancellationToken)
        {
            using (var session = _documentStore.Store.OpenAsyncSession())
            {
                var user = new UserDocument(userRegistered.Name, userRegistered.LastName, userRegistered.Email, userRegistered.Password);

                await session.StoreAsync(user);
                await session.SaveChangesAsync();
            }
        }
    }
}
