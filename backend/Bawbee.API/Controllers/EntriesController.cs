using Bawbee.Application.Adapters;
using Bawbee.Application.Commands;
using Bawbee.Application.CommandStack.Entries.Commands;
using Bawbee.Application.CommandStack.Entries.InputModels;
using Bawbee.Application.QueryStack.Users.Queries.Entries;
using Bawbee.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class EntriesController : BaseApiController
    {
        private readonly IMediatorHandler _mediator;

        public EntriesController(
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _mediator = mediator;
        }

        #region Expenses

        [HttpGet("expenses/month/{month:int}")]
        public async Task<IActionResult> GetExpensesByUser(int month)
        {
            var query = new GetMonthEntriesQuery(CurrentUserId, month);

            if (!query.IsValid())
                return CustomResponse(query.ValidationResult);

            var expenses = await _mediator.SendCommand(query);

            return CustomResponse(expenses);
        }

        //[ProducesResponseType()] // TODO:
        [HttpGet("expenses/reports/month/{month:int}")]
        public async Task<IActionResult> GetTotalExpensesGroupedByMonth(int month)
        {
            var query = new GetTotalExpensesGroupedByMonthQuery(month, CurrentUserId);

            if (!query.IsValid())
                return CustomResponse(query.ValidationResult);

            var expenses = await _mediator.SendCommand(query);

            return CustomResponse(expenses);
        }

        [HttpPost("expenses")]
        public async Task<IActionResult> CreateExpense(CreateExpenseInputModel model)
        {
            var command = model.MapToCreateExpenseCommand(CurrentUserId);

            if (!command.IsValid())
                return CustomResponse(command.ValidationResult);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }

        [HttpPut("expenses/{id:int}")]
        public async Task<IActionResult> UpdateExpense(int id, UpdateExpenseInputModel model)
        {
            var command = model.MapToUpdateExpenseCommand(id, CurrentUserId);

            if (!command.IsValid())
                return CustomResponse(command.ValidationResult);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }

        [HttpDelete("expenses/{id:int}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var command = new DeleteExpenseCommand(id, CurrentUserId);

            if (!command.IsValid())
                return CustomResponse(command.ValidationResult);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }

        #endregion

        #region Incomes

        [HttpGet("incomes")]
        public async Task<IActionResult> GetIncomesByUser()
        {
            return Ok();
        }

        [HttpPost("incomes")]
        public async Task<IActionResult> AddIncome(CreateExpenseInputModel model)
        {
            return Ok();
        }

        [HttpPut("incomes/{id:int}")]
        public async Task<IActionResult> UpdateIncome(UpdateExpenseInputModel model)
        {
            return Ok();
        }

        [HttpDelete("incomes/{id:int}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            return Ok();
        }

        #endregion
    }
}
