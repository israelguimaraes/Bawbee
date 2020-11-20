using Bawbee.Application.CommandStack.Expenses.InputModels;
using Bawbee.Core.Bus;
using Bawbee.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class IncomesController : BaseApiController
    {
        private readonly IMediatorHandler _mediator;

        public IncomesController(
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetIncomesByUser()
        {
            return Ok();
        }

        [HttpPost("")]
        public async Task<IActionResult> AddIncome(CreateExpenseInputModel model)
        {
            return Ok();
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateIncome(UpdateExpenseInputModel model)
        {
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            return Ok();
        }
    }
}
