using Bawbee.Application.QueryStack.Users.Queries.Entries;
using Bawbee.Core.Bus;
using Bawbee.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
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

        [HttpGet("recreate-database/insert-initial-data")]
        public async Task<IActionResult> RecreateDatabaseAndSetInitialData()
        {
            var query = new GetAllExpensesByUserQuery(CurrentUserId);

            if (!query.IsValid())
                return CustomResponse(query.ValidationResult);

            var expenses = await _mediator.SendCommand(query);

            return Ok();
        }
    }
}
