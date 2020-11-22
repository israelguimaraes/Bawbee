using Bawbee.Application.CommandStack.Admin.Commands;
using Bawbee.Core.Bus;
using Bawbee.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    [AllowAnonymous]
    public class AdminController : BaseApiController
    {
        private readonly IMediatorHandler _mediator;

        public AdminController(
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _mediator = mediator;
        }

        [HttpGet("database/recreate/insert-initial-data")]
        public async Task<IActionResult> RecreateDatabaseAndSetInitialData()
        {
            var result = await _mediator.SendCommand(new RecreateDatabaseAndSetInitialDataCommand());

            return Ok(result);
        }
    }
}
