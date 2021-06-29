using Bawbee.Application.Operations;
using Bawbee.SharedKernel.Notifications;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Bawbee.API.Controllers
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
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
            if (operationResult.Status == Status.Ok)
                return Ok(operationResult.Data);

            // 204
            if (operationResult.Status == Status.OkWithoutReturn)
                return NoContent();

            // 400
            if (operationResult.Status == Status.InvalidOperation)
            {
                object data = _notificationHandler.HasNotifications ? _notificationHandler.GetMessageErrors() : (object)operationResult.Message;
                return BadRequest(data);
            }

            // 404
            if (operationResult.Status == Status.NotFoundData)
                return NotFound(operationResult.Message);

            // 500
            if (operationResult?.Status == Status.ApplicationError)
                return StatusCode((int)Status.ApplicationError, "Internal server error.");

            throw new InvalidOperationException();
        }

        protected IActionResult BadRequestResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notificationHandler.AddNotification(error.ErrorMessage);
            }

            return BadRequest(new
            {
                Errors = _notificationHandler.Notifications.Select(n => n.Message)
            });
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
