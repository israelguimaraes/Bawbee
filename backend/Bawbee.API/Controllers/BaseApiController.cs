using Bawbee.Application.Operations;
using Bawbee.SharedKernel.Notifications;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Bawbee.API.Controllers
{
    [Authorize("Bearer")]
    //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseApiController : Controller
    {
        private readonly DomainNotificationHandler _notificationHandler;
        private ClaimsPrincipal _userPrincipal;

        protected BaseApiController(INotificationHandler<DomainNotification> notificationHandler)
        {
            _notificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        protected IEnumerable<DomainNotification> Notifications => _notificationHandler.Notifications;
        protected bool IsValidOperation => !Notifications.Any();

        protected IActionResult CustomResponse(OperationResult operationResult)
        {
            // 200
            if (operationResult.Type == StatusResult.Ok)
                return Ok(operationResult.Data);

            // 204
            if (operationResult.Type == StatusResult.OkWithoutReturn)
                return NoContent();

            // 400
            if (operationResult.Type == StatusResult.InvalidOperation)
                return BadRequest(operationResult.Message);

            // 404
            if (operationResult.Type == StatusResult.NotFoundData)
                return NotFound(operationResult.Message);

            // 500
            if (operationResult?.Type == StatusResult.ApplicationError)
                return StatusCode((int)StatusResult.ApplicationError, "Internal server error.");

            throw new InvalidOperationException();
        }

        protected IActionResult BadRequestResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notificationHandler.AddNotification(error.ErrorMessage);
            }

            return BadRequest();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _userPrincipal = User;
            base.OnActionExecuting(context);
        }

        protected int CurrentUserId
        {
            get
            {
                var userId = _userPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                return int.Parse(userId);
            }
        }
    }
}
