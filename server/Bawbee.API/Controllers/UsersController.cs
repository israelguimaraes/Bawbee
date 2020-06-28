using Bawbee.Application.Users.Interfaces;
using Bawbee.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserApplication _userApplication;

        public UsersController(
            IUserApplication userApplication,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _userApplication = userApplication;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsersTest()
        {
            var users = await _userApplication.GetAll();
            return Response(users);
        }
    }
}
