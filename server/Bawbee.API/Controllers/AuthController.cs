using Bawbee.Application.CommandStack.Users.Commands;
using Bawbee.Application.CommandStack.Users.InputModels;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IMediatorHandler _mediator;

        public AuthController(
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser(RegisterNewUserInputModel model)
        {
            var command = new RegisterNewUserCommand(model.Name, model.LastName, model.Email, model.Password);

            if (!command.IsValid())
                return Response(command);

            var result = await _mediator.SendCommand(command);
            return Response(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            var command = new LoginCommand(model.Email, model.Password);

            if (!command.IsValid())
                return Response(command);

            var result = await _mediator.SendCommand(command);
            return Response(result);
        }
    }
}
