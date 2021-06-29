using Bawbee.Application.Bus;
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
    public class LoginCommandHandler : BaseCommandHandler, ICommandHandler<LoginCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommandBus _bus;
        private readonly ISecurityTokenService _securityTokenService;

        public LoginCommandHandler(
            ICommandBus bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notificationHandler,
            IUserRepository userRepository,
            ISecurityTokenService securityTokenService) : base(bus, unitOfWork, notificationHandler)
        {
            _bus = bus;
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
            await _bus.PublishEvent(@event);

            return Ok(userAccessToken);
        }
    }
}
