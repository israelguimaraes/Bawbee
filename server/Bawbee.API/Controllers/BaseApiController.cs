using Bawbee.Core.Commands;
using Bawbee.Core.Notifications;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        protected IEnumerable<DomainNotification> GetNotifications => _notificationHandler.GetNotifications;

        protected bool IsValidOperation => !GetNotifications.Any();

        protected IActionResult CustomResponse(Operation command)
        {
            switch (command.OperationResult)
            {
                case OperationEnum.Create:
                    break;
                case OperationEnum.Update:
                    break;
                case OperationEnum.UpdateReturn:
                    break;
                case OperationEnum.Delete:
                    break;
                case OperationEnum.Read:
                    break;
                case OperationEnum.BadRead:
                    break;
                case OperationEnum.BadRequest:
                    break;
                case OperationEnum.Error:
                    break;
                default:
                    break;
            }

            return BadRequest();
        }

        //#region MyRegion

        //protected IActionResult CustomResponse(object data)
        //{
        //    if (IsValidOperation)
        //        return Ok(data);

        //    return BadRequestResponse();
        //}

        //protected IActionResult CustomResponse(CommandResult commandResult = null)
        //{
        //    if (IsValidOperation && commandResult.Success)
        //        return Ok(commandResult);

        //    return BadRequestResponse();
        //}

        //private IActionResult BadRequestResponse(ValidationResult validationResult)
        //{
        //    foreach (var error in validationResult.Errors)
        //    {
        //        _notificationHandler.AddNotification(error.ErrorMessage);
        //    }

        //    return BadRequestResponse();
        //}

        //private IActionResult BadRequestResponse()
        //{
        //    var notifications = GetNotifications.Select(n => n.Value);
        //    var result = CommandResult.Error(notifications);

        //    return BadRequest(result);
        //}

        //#endregion

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
