using Bawbee.Domain.Events;
using Bawbee.Domain.Events.BankAccounts;
using Bawbee.Domain.Events.Categories;
using Bawbee.Infra.Data.Adapters;
using Bawbee.Infra.Data.Documents;
using Bawbee.Infra.Data.Documents.Users;
using Bawbee.Infra.Data.ReadInterfaces;
using MediatR;
using Raven.Client.Documents.Session;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Infra.Data.RavenDB.EventHandlers
{
    public class UserRavenDBHandler :
        INotificationHandler<UserRegisteredEvent>,
        INotificationHandler<CategoryCreatedEvent>,
        INotificationHandler<BankAccountCreatedEvent>
    {
        private readonly IAsyncDocumentSession _session;
        private readonly IUserReadRepository _userReadRepository;

        public UserRavenDBHandler(IAsyncDocumentSession session, IUserReadRepository userReadRepository)
        {
            _session = session;
            _userReadRepository = userReadRepository;
        }

        public async Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken)
        {
            var userDocument = @event.MapToUserDocument();

            await _session.StoreAsync(userDocument);
        }

        public async Task Handle(CategoryCreatedEvent @event, CancellationToken cancellationToken)
        {
            var userDocument = await _userReadRepository.GetByUserId(@event.UserId);

            userDocument.Categories.Add(new Category
            {
                CategoryId = @event.CategoryId,
                Name = @event.Name
            });

            await _session.StoreAsync(userDocument);
        }

        public async Task Handle(BankAccountCreatedEvent @event, CancellationToken cancellationToken)
        {
            var userDocument = await _userReadRepository.GetByUserId(@event.UserId);

            userDocument.BankAccounts.Add(new BankAccount
            {
                BankAccountId = @event.BankAccountId,
                Name = @event.Name,
                InitialBalance = @event.InitialBalance,
                CreatedAt = @event.Timestamp
            });

            await _session.StoreAsync(userDocument);
        }
    }
}