using Bawbee.Application.Users.InputModels.Entries;
using Bawbee.Application.Users.Interfaces;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Infra.CrossCutting.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class ExpensesController : BaseApiController
    {
        private readonly IEntryApplication _entryApplication;

        public ExpensesController(
            IEntryApplication entryApplication,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _entryApplication = entryApplication;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetExpensesByUser()
        {
            var result = await _entryApplication.GetAllByUser(CurrentUserId);
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
