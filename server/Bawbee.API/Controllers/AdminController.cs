using Bawbee.Application.CommandStack.Admin.Commands;
using Bawbee.Application.CommandStack.Expenses.Commands;
using Bawbee.Application.CommandStack.Users.Commands;
using Bawbee.Core.Bus;
using Bawbee.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    [AllowAnonymous]
    public class AdminController : BaseApiController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IServiceProvider _serviceProvider;

        public AdminController(
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notificationHandler,
            IServiceProvider serviceProvider)
            : base(notificationHandler)
        {
            _mediator = mediator;
            _serviceProvider = serviceProvider;
        }

        [HttpGet("database/recreate/insert-initial-data")]
        public async Task<IActionResult> RecreateDatabaseAndSetInitialData()
        {
            // recreate database
            var result = await _mediator.SendCommand(new RecreateDatabaseAndSetInitialDataCommand());

            // create user
            var newUserCommand = new CreateUserCommand("Israel", "Guimarães", "israel@gmail.com", "123456");
            var resultUser = await _mediator.SendCommand(newUserCommand);

            // create expenses
            for (int i = 0; i < 10; i++)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var command = new CreateExpenseCommand(
                        $"description {i}",
                        (i * 1.45m),
                        true, null,
                        DateTime.Now.AddDays(-i), default, default, default);

                    await _serviceProvider.GetRequiredService<IMediatorHandler>().SendCommand(command);
                }
            }

            return Ok(result);
        }
    }
}
