using Bawbee.Application.Interfaces;
using Bawbee.Application.ViewModels.Users;
using Bawbee.Domain.Commands.Users;
using Bawbee.Domain.Core.Bus;
using System;
using System.Threading.Tasks;

namespace Bawbee.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IMediatorHandler _mediator;

        public UserApplication(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        public async Task Register(RegisterUserViewModel viewModel)
        {
            var command = new RegisterNewUserCommand(viewModel.Name, viewModel.LastName, viewModel.Email, viewModel.Password);
            await _mediator.SendCommand(command);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
