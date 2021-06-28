using Bawbee.Application.Mediator;
using Bawbee.Application.Operations;
using Bawbee.Core;
using Bawbee.Core.Aggregates.Users;
using Bawbee.Core.Aggregates.Users.Events;
using Bawbee.Infrastructure.Security.Jwt;
using Bawbee.SharedKernel.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.UseCases.Users.LoginUser
{
    public class LoginCommandHandler : BaseCommandHandler,
        IRequestHandler<LoginCommand, OperationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _mediator;
        private readonly ISecurityTokenService _securityTokenService;

        public LoginCommandHandler(
            IMediatorHandler mediator,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notificationHandler,
            IUserRepository userRepository,
            ISecurityTokenService securityTokenService) : base(mediator, unitOfWork, notificationHandler)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _securityTokenService = securityTokenService;
        }

        public async Task<OperationResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAndPassword(command.Email, command.Password);

            if (user == null)
            {
                await AddNotification("E-mail or password is invalid");
                return Invalid();
            }

            var userAccessToken = _securityTokenService.GenerateToken(user.Id, user.Name, user.Email);

            var @event = new UserLoggedEvent(user.Id, user.Name, user.Email);
            await _mediator.PublishEvent(@event);

            return Ok(userAccessToken);
        }
    }
}
