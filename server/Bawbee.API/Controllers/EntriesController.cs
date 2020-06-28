using Bawbee.Application.Users.InputModels.Entries;
using Bawbee.Application.Users.Interfaces;
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
            var result = await _entryApplication.AddNewEntry(model);
            return Response(result);
        }
    }
}
