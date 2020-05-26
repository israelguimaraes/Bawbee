using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bawbee.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bawbee.API.Controllers
{
    public class AuthController : BaseApiController
    {
        public AuthController() : base(new DomainNotificationHandler())
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Response("ok test");
        }
    }
}
