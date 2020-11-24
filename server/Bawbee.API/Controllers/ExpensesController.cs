using Bawbee.Application.Adapters;
using Bawbee.Application.CommandStack.Entries.Commands;
using Bawbee.Application.CommandStack.Entries.InputModels;
using Bawbee.Application.QueryStack.Users.Queries.Entries;
using Bawbee.Core.Bus;
using Bawbee.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class ExpensesController : BaseApiController
    {
        private readonly IMediatorHandler _mediator;

        public ExpensesController(
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetExpensesByUser()
        {
            var query = new GetAllExpensesByUserQuery(CurrentUserId);

            if (!query.IsValid())
                return CustomResponse(query.ValidationResult);

            var expenses = await _mediator.SendCommand(query);

            return CustomResponse(expenses);
        }

        [HttpGet("month/{month:int}")]
        public async Task<IActionResult> GetTotalExpensesGroupedByMonth(int month)
        {
            var query = new GetTotalExpensesGroupedByMonthQuery(month, CurrentUserId);

            if (!query.IsValid())
                return CustomResponse(query.ValidationResult);

            var expenses = await _mediator.SendCommand(query);

            return CustomResponse(expenses);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateExpense(CreateExpenseInputModel model)
        {
            var command = model.MapToCreateExpenseCommand(CurrentUserId);

            if (!command.IsValid())
                return CustomResponse(command.ValidationResult);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateExpense(int id, UpdateExpenseInputModel model)
        {
            var command = model.MapToUpdateExpenseCommand(id, CurrentUserId);

            if (!command.IsValid())
                return CustomResponse(command.ValidationResult);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var command = new DeleteExpenseCommand(id, CurrentUserId);

            if (!command.IsValid())
                return CustomResponse(command.ValidationResult);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }
    }
}
