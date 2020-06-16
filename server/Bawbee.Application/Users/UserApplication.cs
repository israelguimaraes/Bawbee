using Bawbee.Application.Command.Users;
using Bawbee.Application.Query.Users.Queries;
using Bawbee.Application.Query.Users.ReadModels;
using Bawbee.Application.Users.InputModels;
using Bawbee.Application.Users.Interfaces;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using System;
using System.Collections.Generic;
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
            //return result = await _mediator.SendCommand(command);
            await _mediator.SendCommand(command);
        }

        public Task<IEnumerable<UserReadModel>> GetAll()
        {
            var query = new GetAllUsersQuery();
            return _mediator.SendCommand(query);
        }

        public async Task<CommandResult> Login(LoginInputModel model)
        {
            var command = new LoginCommand(model.Email, model.Password);
            return await _mediator.SendCommand(command);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
