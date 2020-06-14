using Bawbee.Application.InputModels.Users;
using Bawbee.Application.Interfaces;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Infra.CrossCutting.Common.Security;
using Bawbee.Infra.CrossCutting.Common.Security.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IUserApplication _userApplication;
        private readonly IConfiguration _configuration;

        public AuthController(
            IUserApplication userApplication,
            INotificationHandler<DomainNotification> notificationHandler,
            IConfiguration configuration)
            : base(notificationHandler)
        {
            _userApplication = userApplication;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("token")]
        public async Task<IActionResult> GetRandomToken()
        {
            var jwt = new JwtService(_configuration);
            var token = jwt.GenerateSecurityToken(new UserTokenDTO { Email = "admin@gmail.com", Name = "Israel", UserId = 1 });

            return Response(token);
        }


        [HttpGet("login")]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            //var userToken = await _userApplication.Login(model);
            //return Response(userToken);
            return null;
        }

        [AllowAnonymous]
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
