using Bawbee.Application.CommandStack.Users.InputModels.Entries;
using Bawbee.Application.Users.Interfaces;
using Bawbee.Core.Bus;
using Bawbee.Core.Notifications;
using Bawbee.Infra.CrossCutting.Extensions;
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
            var result = await _entryApplication.GetAllByUser(CurrentUserId);
            return Response(result);
        }

        [HttpGet("month/{month:int}")]
        public async Task<IActionResult> GetTotalExpensesGroupedByMonth(int month)
        {
            var result = await _entryApplication.GetTotalExpensesGroupedByMonth(month, CurrentUserId);
            return Response(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddExpense(NewEntryInputModel model)
        {
            model.Value = model.Value.ToNegative();

            var result = await _entryApplication.AddEntry(model, CurrentUserId);
            return Response(result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateExpense(UpdateEntryInputModel model)
        {
            model.Value = model.Value.ToNegative();

            var result = await _entryApplication.Update(model, CurrentUserId);
            return Response(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var result = await _entryApplication.Delete(id, CurrentUserId);
            return Response(result);
        }
    }
}
