﻿using Bawbee.Application.Bus;
using Bawbee.Core;
using Bawbee.Core.Aggregates.Users;
using Bawbee.SharedKernel.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    [AllowAnonymous]
    public class AdminController : BaseApiController
    {
        private readonly ICommandBus _bus;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork _uow;

        public AdminController(
            ICommandBus bus,
            INotificationHandler<DomainNotification> notificationHandler,
            IServiceProvider serviceProvider,
            IUnitOfWork uow)
            : base(notificationHandler)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
            _uow = uow;
        }

        //[HttpGet("database/recreate/insert-initial-data")]
        //public async Task<IActionResult> RecreateDatabaseAndSetInitialData()
        //{
        //    var totalTime = Stopwatch.StartNew();

        //    // recreate database
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var result = await _mediator.SendCommand(new RecreateDatabaseCommand());
        //    }

        //    // create user
        //    CommandResult resultUser;
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var newUserCommand = new CreateUserCommand("Israel", "Guimarães", "israel@gmail.com", "123456");
        //        resultUser = await _mediator.SendCommand(newUserCommand);
        //    }
            
        //    var expensesTime = Stopwatch.StartNew();

        //    // create expenses
        //    var user = resultUser.Data as User;

        //    var today = DateTime.Now;

        //    for (int i = 1; i < 10; i++)
        //    {
        //        using (var scope = _serviceProvider.CreateScope())
        //        {
        //            var command = new CreateExpenseCommand(
        //                $"description {i}",
        //                (i * 1.45m),
        //                true, null,
        //                new DateTime(today.Year,today.Month, i, today.Hour, today.Minute, today.Second, today.Millisecond),
        //                user.Id,
        //                1, 
        //                (i+ 1));

        //            var mediator = _serviceProvider.GetRequiredService<IMediatorHandler>();
        //            await mediator.SendCommand(command);
        //        }
        //    }

        //    expensesTime.Stop();
        //    totalTime.Stop();

        //    return Ok(new
        //    {
        //        expensesTime = TimeSpan.FromMilliseconds(expensesTime.ElapsedMilliseconds).TotalSeconds,
        //        totalTime = TimeSpan.FromMilliseconds(totalTime.ElapsedMilliseconds).TotalSeconds
        //    });
        //}
    }
}
