using Bawbee.Core.Commands;
using Bawbee.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Bawbee.API.Controllers
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
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

        protected IEnumerable<DomainNotification> GetNotifications => _notificationHandler.GetNotifications;

        protected bool IsValidOperation => !GetNotifications.Any();

        protected new IActionResult Response(object data)
        {
            if (IsValidOperation)
                return OkResponse(data);

            return BadRequestResponse();
        }

        protected new IActionResult Response(CommandResult commandResult = null)
        {
            if (IsValidOperation && commandResult.IsSuccess)
                return OkResponse(commandResult);

            return BadRequestResponse();
        }

        private IActionResult OkResponse(object data)
        {
            return Ok(data);
        }

        private IActionResult BadRequestResponse()
        {
            var result = CommandResult.Error(GetNotifications.Select(n => n.Value));

            return BadRequest(result);
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
