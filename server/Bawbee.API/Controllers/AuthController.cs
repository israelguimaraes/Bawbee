using Bawbee.Application.InputModels.Users;
using Bawbee.Application.Interfaces;
using Bawbee.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IUserApplication _userApplication;

        public AuthController(
            IUserApplication userApplication,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _userApplication = userApplication;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterNewUser(RegisterNewUserInputModel model)
        {
            await _userApplication.Register(model);

            return Response("ok test");
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsersTest()
        {
            var users = await _userApplication.GetAll();
            return Response(users);
        }
    }
}
