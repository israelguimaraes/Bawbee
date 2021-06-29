using Bawbee.API.Requests.Categories;
using Bawbee.API.Requests.Users;
using Bawbee.Application.Bus;
using Bawbee.Application.UseCases.Categories.CreateCategory;
using Bawbee.Application.UseCases.Categories.GetAllCategoriesByUser;
using Bawbee.Application.UseCases.Users;
using Bawbee.Application.UseCases.Users.LoginUser;
using Bawbee.SharedKernel.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly ICommandBus _bus;

        public UsersController(
            ICommandBus bus,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _bus = bus;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var command = new CreateUserCommand(request.Name, request.LastName, request.Email, request.Password, request.ConfirmPassword);

            if (!command.IsValid())
                return BadRequestResponse(command.ValidationResult);

            var result = await _bus.SendCommand(command);
            return CustomResponse(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new LoginCommand(request.Email, request.Password);

            if (!command.IsValid())
                return BadRequestResponse(command.ValidationResult);

            var result = await _bus.SendCommand(command);
            return CustomResponse(result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategoriesByUser()
        {
            var query = new GetAllCategoriesByUserQuery(CurrentUserId);

            var result = await _bus.SendCommand(query);
            return CustomResponse(result);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            var command = new CreateCategoryCommand(request.Name, CurrentUserId);

            if (!command.IsValid())
                return BadRequestResponse(command.ValidationResult);

            var result = await _bus.SendCommand(command);

            return CustomResponse(result);
        }
    }
}
