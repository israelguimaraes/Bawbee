using Bawbee.Application.ViewModels.Users;
using Bawbee.Domain.Entities;

namespace Bawbee.Application.Adapters
{
    public static class UserAdapter
    {
        public static User MapToDomain(this RegisterUserViewModel viewModel)
        {
            return new User(viewModel.Name, viewModel.LastName, viewModel.Email, viewModel.Password);
        }
    }
}
