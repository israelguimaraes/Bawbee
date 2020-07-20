using Bawbee.Application.Command.Users;
using Bawbee.Application.Command.Users.BankAccounts;
using Bawbee.Application.Command.Users.Categories;
using Bawbee.Application.Query.Users.Queries;
using Bawbee.Application.Query.Users.ReadModels;
using Bawbee.Application.Users.InputModels;
using Bawbee.Application.Users.InputModels.BankAccounts;
using Bawbee.Application.Users.InputModels.Categories;
using Bawbee.Application.Users.Interfaces;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
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

        public async Task<CommandResult> Register(RegisterNewUserInputModel model)
        {
            var command = new RegisterNewUserCommand(model.Name, model.LastName, model.Email, model.Password);

            if (!command.IsValid())
            {
                SendNotificationsErrors(command);
                return CommandResult.Error();
            }

            return await _mediator.SendCommand(command);
        }

        public Task<IEnumerable<UserReadModel>> GetAll()
        {
            var query = new GetAllUsersQuery();
            return _mediator.SendCommand(query);
        }

        public Task<IEnumerable<EntryCategoryReadModel>> GetCategories(int userId)
        {
            var query = new GetAllCategoriesByUserQuery(userId);
            return _mediator.SendCommand(query);
        }

        public Task<IEnumerable<BankAccountReadModel>> GetBankAccounts(int userId)
        {
            var query = new GetAllBankAccountsByUserQuery(userId);
            return _mediator.SendCommand(query);
        }

        public async Task<CommandResult> AddCategory(AddEntryCategoryInputModel model, int userId)
        {
            var command = new AddEntryCategoryCommand(model.Name, userId);
            
            if (!command.IsValid())
            {
                SendNotificationsErrors(command);
                return CommandResult.Error();
            }

            return await _mediator.SendCommand(command);
        }

        public async Task<CommandResult> AddBankAccount(AddBankAccountInputModel model, int userId)
        {
            var command = new AddBankAccountCommand(model.Name, model.InitialBalance, userId);

            if (!command.IsValid())
            {
                SendNotificationsErrors(command);
                return CommandResult.Error();
            }

            return await _mediator.SendCommand(command);
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
