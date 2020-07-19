using Bawbee.Application.Command.Entries;
using Bawbee.Application.Query.Users.Queries.Entries;
using Bawbee.Application.Users.InputModels.Entries;
using Bawbee.Application.Users.Interfaces;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using System;
using System.Threading.Tasks;

namespace Bawbee.Application.Entries
{
    public class EntryApplication : IEntryApplication, IDisposable
    {
        private readonly IMediatorHandler _mediator;

        public EntryApplication(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResult> AddNewEntry(NewEntryInputModel model)
        {
            var command = new NewEntryCommand(
                model.UserId, model.Description, model.Value, model.IsPaid,
                model.Observations, model.DateToPay, model.BankAccountId, model.EntryCategoryId);

            if (!command.IsValid())
            {
                SendNotificationsErrors(command);
                return CommandResult.Error();
            }

            return await _mediator.SendCommand(command);
        }

        public async Task<CommandResult> Update(UpdateEntryInputModel model)
        {
            var command = new UpdateEntryCommand(
                model.EntryId, model.UserId, model.Description, model.Value, model.IsPaid,
                model.Observations, model.DateToPay, model.BankAccountId, model.EntryCategoryId);

            if (!command.IsValid())
            {
                SendNotificationsErrors(command);
                return CommandResult.Error();
            }

            return await _mediator.SendCommand(command);
        }

        public async Task<CommandResult> GetAllByUser(int userId)
        {
            var query = new GetAllEntriesByUser(userId);
            var entries = await _mediator.SendCommand(query);

            return CommandResult.Ok(entries);
        }

        // TODO: create in BaseApplication
        private void SendNotificationsErrors(BaseCommand message)
        {
            foreach (var error in message.ValidationResult.Errors)
                _mediator.PublishEvent(new DomainNotification(error.ErrorMessage));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<CommandResult> Delete(int entryId, int userId)
        {
            var command = new DeleteEntryCommand(entryId, userId);

            if (!command.IsValid())
            {
                SendNotificationsErrors(command);
                return CommandResult.Error();
            }

            return await _mediator.SendCommand(command);
        }
    }
}
