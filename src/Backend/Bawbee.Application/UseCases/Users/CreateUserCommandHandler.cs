using Bawbee.Application.Mediator;
using Bawbee.Application.Operations;
using Bawbee.Application.UseCases.Users.Mappers;
using Bawbee.Core;
using Bawbee.Core.Aggregates.Users;
using Bawbee.Core.Aggregates.Users.Events;
using Bawbee.SharedKernel.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.UseCases.Users
{
    public class CreateUserCommandHandler : BaseCommandHandler,
        IRequestHandler<CreateUserCommand, OperationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _mediator;

        public CreateUserCommandHandler(
            IMediatorHandler mediator,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notificationHandler, 
            IUserRepository userRepository) : base(mediator, unitOfWork, notificationHandler)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(command.Email);

            if (user != null)
            {
                await AddNotification("E-mail already used");
                return Invalid();
            }

            var newUser = User.UserFactory.CreateNewPlataformUser(command.Name, command.LastName, command.Email, command.Password);

            await _userRepository.Add(newUser);

            if (await CommitTransaction())
            {
                UserRegisteredEvent @event = UserMapper.Map(newUser);
                await _mediator.PublishEvent(@event);
            }

            return Ok();
        }
    }
}
