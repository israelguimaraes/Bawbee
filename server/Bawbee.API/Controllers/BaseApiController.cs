using Bawbee.Domain.Core.Notifications;
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

        protected new IActionResult Response(object data = null)
        {
            if (IsValidOperation)
                return OkResponse(data);

            return BadRequestResponse();
        }

        private IActionResult OkResponse(object data)
        {
            return Ok(new { success = true, data });
        }

        private IActionResult BadRequestResponse()
        {
            return BadRequest(new { success = false, errors = GetNotifications.Select(n => n.Value) });
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
