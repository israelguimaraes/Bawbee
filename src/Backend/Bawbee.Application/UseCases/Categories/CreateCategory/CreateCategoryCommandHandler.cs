using Bawbee.Application.Bus;
using Bawbee.Application.Operations;
using Bawbee.Core;
using Bawbee.Core.Aggregates.Entries.Events.Categories;
using Bawbee.Core.Aggregates.Users;
using Bawbee.SharedKernel.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.UseCases.Categories.CreateCategory
{
    public class CreateCategoryCommandHandler : BaseCommandHandler, ICommandHandler<CreateCategoryCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommandBus _bus;

        public CreateCategoryCommandHandler(
            ICommandBus bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notificationHandler,
            IUserRepository userRepository) : base(bus, unitOfWork, notificationHandler)
        {
            _bus = bus;
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _userRepository.GetCategoryByName(command.Name, command.UserId);

            if (category != null)
            {
                await AddNotification($"Category {command.Name} already exists");
                return Invalid();
            }

            category = new Category(command.Name, command.UserId);

            await _userRepository.CreateCategory(category);

            if (!await CommitTransaction())
                return Invalid();

            var @event = new CategoryCreatedEvent(category.Id, category.Name, category.UserId);
            await _bus.PublishEvent(@event);
            return Ok();
        }
    }
}
