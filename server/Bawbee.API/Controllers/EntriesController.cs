using Bawbee.Application.Users.InputModels.Entries;
using Bawbee.Application.Users.Interfaces;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class EntriesController : BaseApiController
    {
        private readonly IEntryApplication _entryApplication;

        public EntriesController(
            IEntryApplication entryApplication,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _entryApplication = entryApplication;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewEntry(NewEntryInputModel model)
        {
            model.UserId = CurrentUserId;

            var result = await _entryApplication.AddNewEntry(model);
            return Response(result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateEntry(UpdateEntryInputModel model)
        {
            model.UserId = CurrentUserId;

            var result = await _entryApplication.Update(model);
            return Response(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var result = await _entryApplication.Delete(id, CurrentUserId);
            return Response(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetEntriesByUser()
        {
            var result = await _entryApplication.GetAllByUser(CurrentUserId);
            return Response(result);
        }
    }
}
