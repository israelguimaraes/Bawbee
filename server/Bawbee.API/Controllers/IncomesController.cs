using Bawbee.Application.Users.InputModels.Entries;
using Bawbee.Application.Users.Interfaces;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class IncomesController : BaseApiController
    {
        private readonly IEntryApplication _entryApplication;

        public IncomesController(
            IEntryApplication entryApplication,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _entryApplication = entryApplication;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetIncomesByUser()
        {
            return Ok();
        }

        [HttpPost("")]
        public async Task<IActionResult> AddIncome(NewEntryInputModel model)
        {
            return Ok();
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateIncome(UpdateEntryInputModel model)
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
