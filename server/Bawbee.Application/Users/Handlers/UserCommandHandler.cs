using Bawbee.Application.Bases;
using Bawbee.Application.Users.Commands;
using Bawbee.Application.Users.Events;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Domain.Entities;
using Bawbee.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.Users.Handlers
{
    public class UserCommandHandler : BaseCommandHandler,
        ICommandHandler<RegisterNewUserCommand>,
        ICommandHandler<LoginCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;

        public UserCommandHandler(
            IMediatorHandler mediator,
            IUserWriteRepository userWriteRepository,
            IUserReadRepository userReadRepository) : base(mediator)
        {
            _mediator = mediator;
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<CommandResult> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid())
            {
                SendNotificationsErrors(command);
                return CommandResult.Error();
            }

            var userDatabase = await _userReadRepository.GetByEmail(command.Email);

            if (userDatabase != null)
            {
                await _mediator.PublishEvent(new DomainNotification("E-mail already used."));
                return CommandResult.Error();
            }

            var user = new User(command.Name, command.LastName, command.Email, command.Password);
            await _userWriteRepository.Add(user);

            await _mediator.PublishEvent(new UserRegisteredEvent(user.UserId, user.Name, user.LastName, user.Email, user.Password));
            return CommandResult.Ok();
        }

        public async Task<CommandResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid())
            {
                SendNotificationsErrors(command);
                return CommandResult.Error();
            }

            var userDatabase = await _userReadRepository.GetByEmailAndPassword(command.Email, command.Password);

            if (userDatabase == null)
            {
                await _mediator.PublishEvent(new DomainNotification("E-mail or password is invalid"));
                return CommandResult.Error();
            }

            await _mediator.PublishEvent(new UserLoggedEvent(userDatabase.UserId, userDatabase.Name, userDatabase.Email));
            return CommandResult.Ok(userDatabase);
        }
    }
}
