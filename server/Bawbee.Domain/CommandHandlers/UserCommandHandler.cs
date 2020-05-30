using Bawbee.Domain.Commands.Users;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Domain.Entities;
using Bawbee.Domain.Events.Users;
using Bawbee.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Domain.CommandHandlers
{
    public class UserCommandHandler : BaseCommandHandler,
        IRequestHandler<RegisterNewUserCommand, bool>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(
            IMediatorHandler mediator, 
            INotificationHandler<DomainNotification> notificationHandler,
            IUserRepository userRepository) : base(mediator, notificationHandler)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid())
            {
                SendNotificationsErrors(command);
                return false;
            }

            var userDatabase = await _userRepository.GetByEmail(command.Email);

            if (userDatabase != null)
            {
                await _mediator.PublishEvent(new DomainNotification("E-mail já está em uso"));
                return false;
            }

            var user = new User(command.Name, command.LastName, command.Email, command.Password);
            await _userRepository.Add(user);

            await _mediator.PublishEvent(new UserRegisteredEvent(user.Id, user.Name, user.LastName, user.Email, user.Password));
            return true;
        }
    }
}
