using Bawbee.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bawbee.API.Controllers
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private readonly DomainNotificationHandler _notificationHandler;

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
    }
}
