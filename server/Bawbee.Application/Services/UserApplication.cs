using Bawbee.Application.InputModels.Users;
using Bawbee.Application.Interfaces;
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

        public async Task Register(RegisterNewUserInputModel model)
        {
            var command = new RegisterNewUserCommand(model.Name, model.LastName, model.Email, model.Password);
            await _mediator.SendCommand(command);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
