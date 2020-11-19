using Bawbee.Application.CommandStack.Expenses.Commands;
using Bawbee.Application.CommandStack.Users.InputModels.Entries;
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
                return CustomResponse(query);

            var expenses = await _mediator.SendCommand(query);

            return CustomResponse(expenses);
        }

        [HttpGet("month/{month:int}")]
        public async Task<IActionResult> GetTotalExpensesGroupedByMonth(int month)
        {
            var query = new GetTotalExpensesGroupedByMonthQuery(month, CurrentUserId);

            if (!query.IsValid())
                return CustomResponse(query);

            var expenses = await _mediator.SendCommand(query);

            return CustomResponse(expenses);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddExpense(CreateExpenseInputModel model)
        {
            var command = new CreateExpenseCommand(
                CurrentUserId, model.Description, model.Value, model.IsPaid, 
                model.Observations, model.DateToPay, model.BankAccountId, model.EntryCategoryId);

            if (!command.IsValid())
                return CustomResponse(command);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateExpense(UpdateExpenseInputModel model)
        {
            var command = new UpdateExpenseCommand(
                model.EntryId, CurrentUserId, model.Description, model.Value, model.IsPaid, 
                model.Observations, model.DateToPay, model.BankAccountId, model.EntryId);

            if (!command.IsValid())
                return CustomResponse(command);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var command = new DeleteExpenseCommand(id, CurrentUserId);

            if (!command.IsValid())
                return CustomResponse(command);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }
    }
}
