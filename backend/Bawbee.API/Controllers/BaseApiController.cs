using Bawbee.Application.Operations;
using Bawbee.SharedKernel.Notifications;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Bawbee.API.Controllers
{
    [Authorize("Bearer")]
    //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseApiController : Controller
    {
        private readonly DomainNotificationHandler _notificationHandler;
        private ClaimsPrincipal _userPrincipal;

        protected BaseApiController(INotificationHandler<DomainNotification> notificationHandler)
        {
            _notificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        protected IEnumerable<DomainNotification> Notifications => _notificationHandler.Notifications;
        protected bool IsValidOperation => !Notifications.Any();

        protected IActionResult CustomResponse(OperationResult operationResult)
        {
            if (operationResult == null)
                return StatusCode(status;

            return BadRequestResponse();
        }

        protected IActionResult CustomResponse(CommandResult commandResult = null)
        {
            if (IsValidOperation && commandResult.Success)
                return Ok(commandResult);

            return BadRequestResponse();
        }

        private IActionResult BadRequestResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notificationHandler.AddNotification(error.ErrorMessage);
            }

            return BadRequestResponse();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _userPrincipal = User;
            base.OnActionExecuting(context);
        }

        protected int CurrentUserId
        {
            get
            {
                var userId = _userPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                return int.Parse(userId);
            }
        }
    }
}
