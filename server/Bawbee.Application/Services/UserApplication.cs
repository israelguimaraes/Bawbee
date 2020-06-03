using Bawbee.Application.InputModels.Users;
using Bawbee.Application.Interfaces;
using Bawbee.Domain.Commands.Users;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Domain.Queries.Users.Queries;
using Bawbee.Domain.Queries.Users.ReadModels;
using MediatR;
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
