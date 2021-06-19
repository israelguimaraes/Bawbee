using Bawbee.Application.Mediator;
using Bawbee.Application.Operations;
using Bawbee.Application.UseCases.Users.Mappers;
using Bawbee.Core;
using Bawbee.Core.Aggregates.Users;
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
            INotificationHandler<DomainNotification> notificationHandler) : base(mediator, unitOfWork, notificationHandler)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userDatabase = await _userRepository.GetByEmail(command.Email);

            if (userDatabase != null)
            {
                await AddNotification("E-mail already used");
                return Invalid();
            }

            var user = User.UserFactory.CreateNewPlataformUser(command.Name, command.LastName, command.Email, command.Password);

            await _userRepository.Add(user);

            if (await CommitTransaction())
            {
                var userRegisteredEvent = user.MapToUserRegisteredEvent();
                await _mediator.PublishEvent(userRegisteredEvent);
            }

            return Ok();
        }
    }
}
