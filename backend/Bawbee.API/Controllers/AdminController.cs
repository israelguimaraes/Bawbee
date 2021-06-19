//using Bawbee.Application.Commands;
//using Bawbee.Application.CommandStack.Admin.Commands;
//using Bawbee.Application.CommandStack.Entries.Commands;
//using Bawbee.Application.CommandStack.Users.Commands;
//using Bawbee.Core;
//using Bawbee.Core.Aggregates.Users;
//using Bawbee.Core.Commands;
//using Bawbee.Core.Notifications;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//namespace Bawbee.API.Controllers
//{
//    [AllowAnonymous]
//    public class AdminController : BaseApiController
//    {
//        private readonly IMediatorHandler _mediator;
//        private readonly IServiceProvider _serviceProvider;
//        private readonly IUnitOfWork _uow;

//        public AdminController(
//            IMediatorHandler mediator,
//            INotificationHandler<DomainNotification> notificationHandler,
//            IServiceProvider serviceProvider,
//            IUnitOfWork uow)
//            : base(notificationHandler)
//        {
//            _mediator = mediator;
//            _serviceProvider = serviceProvider;
//            _uow = uow;
//        }

//        [HttpGet("database/recreate/insert-initial-data")]
//        public async Task<IActionResult> RecreateDatabaseAndSetInitialData()
//        {
//            var totalTime = Stopwatch.StartNew();

//            // recreate database
//            using (var scope = _serviceProvider.CreateScope())
//            {
//                var result = await _mediator.SendCommand(new RecreateDatabaseCommand());
//            }

//            // create user
//            CommandResult resultUser;
//            using (var scope = _serviceProvider.CreateScope())
//            {
//                var newUserCommand = new CreateUserCommand("Israel", "Guimarães", "israel@gmail.com", "123456");
//                resultUser = await _mediator.SendCommand(newUserCommand);
//            }
            
//            var expensesTime = Stopwatch.StartNew();

//            // create expenses
//            var user = resultUser.Data as User;

//            var today = DateTime.Now;

//            for (int i = 1; i < 10; i++)
//            {
//                using (var scope = _serviceProvider.CreateScope())
//                {
//                    var command = new CreateExpenseCommand(
//                        $"description {i}",
//                        (i * 1.45m),
//                        true, null,
//                        new DateTime(today.Year,today.Month, i, today.Hour, today.Minute, today.Second, today.Millisecond),
//                        user.Id,
//                        1, 
//                        (i+ 1));

//                    var mediator = _serviceProvider.GetRequiredService<IMediatorHandler>();
//                    await mediator.SendCommand(command);
//                }
//            }

//            expensesTime.Stop();
//            totalTime.Stop();

//            return Ok(new
//            {
//                expensesTime = TimeSpan.FromMilliseconds(expensesTime.ElapsedMilliseconds).TotalSeconds,
//                totalTime = TimeSpan.FromMilliseconds(totalTime.ElapsedMilliseconds).TotalSeconds
//            });
//        }
//    }
//}
