using Bawbee.Application.CommandStack.Users.Commands;
using Bawbee.Application.CommandStack.Users.InputModels;
using Bawbee.Application.CommandStack.Users.InputModels.BankAccounts;
using Bawbee.Application.CommandStack.Users.InputModels.Categories;
using Bawbee.Application.QueryStack.Users.Queries;
using Bawbee.Core.Bus;
using Bawbee.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IMediatorHandler _mediator;

        public UsersController(
            IMediatorHandler mediatorHandler,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _mediator = mediatorHandler;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Create(CreateUserInputModel model)
        {
            var command = new CreateUserCommand(model.Name, model.LastName, model.Email, model.Password);

            if (!command.IsValid())
                return CustomResponse(command);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            var command = new LoginCommand(model.Email, model.Password);

            if (!command.IsValid())
                return CustomResponse(command);

            var result = await _mediator.SendCommand(command);
            return CustomResponse(result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategoriesByUser()
        {
            var query = new GetAllCategoriesByUserQuery(CurrentUserId);

            var categories = await _mediator.SendCommand(query);

            return CustomResponse(categories);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> AddCategory(CreateCategoryInputModel model)
        {
            var command = new CreateCategoryCommand(model.Name, CurrentUserId);

            if (!command.IsValid())
                return CustomResponse(command);

            var result = await _mediator.SendCommand(command);

            return CustomResponse(result);
        }

        [HttpGet("bank-accounts")]
        public async Task<IActionResult> GetBankAccountsByUser()
        {
            var query = new GetAllBankAccountsByUserQuery(CurrentUserId);

            if (!query.IsValid())
                return CustomResponse(query);

            var bankAccounts = await _mediator.SendCommand(query);

            return CustomResponse(bankAccounts);
        }

        [HttpPost("bank-accounts")]
        public async Task<IActionResult> AddBankAccount(CreateBankAccountInputModel model)
        {
            var command = new CreateBankAccountCommand(model.Name, model.InitialBalance, CurrentUserId);

            if (!command.IsValid())
                return CustomResponse(command);

            var result = await _mediator.SendCommand(command);

            return CustomResponse(result);
        }
    }
}
