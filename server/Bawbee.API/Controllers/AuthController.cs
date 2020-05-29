using Bawbee.Application.Interfaces;
using Bawbee.Application.ViewModels.Users;
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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser(RegisterUserViewModel viewModel)
        {
            await _userApplication.Register(viewModel);

            return await Response("ok test");
        }
    }
}
