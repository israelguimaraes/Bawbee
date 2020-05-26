using Bawbee.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
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

        protected new Task<IActionResult> Response(object data = null)
        {
            if (IsValidOperation)
                return OkResponse(data);

            return BadRequestResponse();
        }

        private async Task<IActionResult> OkResponse(object data)
        {
            return await Task.FromResult(Ok(new { success = true, data }));
        }

        private async Task<IActionResult> BadRequestResponse()
        {
            return await Task.FromResult(
                BadRequest(new { success = false, errors = GetNotifications.Select(n => n.Value) }));
        }
    }
}
