using Bawbee.Application.Commands;
using Bawbee.Application.UseCases.Users;
using Bawbee.SharedKernel.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IMediatorHandler _mediator;

        public UsersController(
            IMediatorHandler mediatorHandler,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _mediator = mediatorHandler;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Create(dynamic model)
        {
            var command = new CreateUserCommand(model.Name, model.LastName, model.Email, model.Password);

            if (!command.IsValid())
                return BadRequestResponse(command.ValidationResult);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }
    }
}
