using Bawbee.Application.Command.Users;
using Bawbee.Application.Query.Users.Queries;
using Bawbee.Application.Query.Users.ReadModels;
using Bawbee.Application.Users.InputModels;
using Bawbee.Application.Users.Interfaces;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Interfaces;
using Bawbee.Domain.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bawbee.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IMediatorHandler _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public UserApplication(IMediatorHandler mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Register(RegisterNewUserInputModel model)
        {
            var command = new RegisterNewUserCommand(model.Name, model.LastName, model.Email, model.Password);

            if (!command.IsValid())
            {
                SendNotificationsErrors(command);
                return CommandResult.Error();
            }

            var commandResult = await _mediator.SendCommand(command);

            if (commandResult.IsSuccess)
            {
                await _unitOfWork.CommitTransaction();
                return commandResult;
            }
            
            return CommandResult.Error();
        }

        public Task<IEnumerable<UserReadModel>> GetAll()
        {
            var query = new GetAllUsersQuery();
            return _mediator.SendCommand(query);
        }

        public async Task<CommandResult> Login(LoginInputModel model)
        {
            var command = new LoginCommand(model.Email, model.Password);

            if (command.IsValid())
            {
                return await _mediator.SendCommand(command);
            }

            SendNotificationsErrors(command);
            return CommandResult.Error();
        }

        private void SendNotificationsErrors(BaseCommand message)
        {
            foreach (var error in message.ValidationResult.Errors)
                _mediator.PublishEvent(new DomainNotification(error.ErrorMessage));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
