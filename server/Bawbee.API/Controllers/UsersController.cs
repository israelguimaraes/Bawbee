using Bawbee.Application.Users.Interfaces;
using Bawbee.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bawbee.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserApplication _userApplication;

        public UsersController(
            IUserApplication userApplication,
            INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _userApplication = userApplication;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategoriesByUser()
        {
            var categories = await _userApplication.GetCategories(CurrentUserId);

            return Response(categories);
        }

        //[HttpGet("bank-accounts")]
        //public async Task<IActionResult> GetBankAccountsByUser()
        //{
        //    var bankAccounts = await _userApplication.GetBankAccounts(CurrentUserId);

        //    return Response(bankAccounts);
        //}
    }
}
