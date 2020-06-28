using Bawbee.Application.Command.Entries;
using Bawbee.Domain.Core.Bus;
using Bawbee.Domain.Core.Commands;
using Bawbee.Domain.Core.Notifications;
using Bawbee.Domain.Core.UnitOfWork;
using Bawbee.Domain.Entities;
using Bawbee.Domain.Events.Entries;
using Bawbee.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.Entries.Handlers
{
    public class EntryCommandHandler : BaseCommandHandler,
        ICommandHandler<NewEntryCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IUserRepository _userRepository;

        public EntryCommandHandler(
            IMediatorHandler mediator, 
            IUnitOfWork unitOfWork, 
            INotificationHandler<DomainNotification> notificationHandler) : base(mediator, unitOfWork, notificationHandler)
        {
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(NewEntryCommand command, CancellationToken cancellationToken)
        {
            var entry = new Entry(
                command.Description, command.Value, command.IsPaid.Value, 
                command.Observations, command.DateToPay, command.BankAccountId, 
                command.EntryCategoryId);

            await _userRepository.AddNewEntry(entry);

            if (await CommitTransaction())
            {
                var userRegisteredEvent = new EntryRegisteredEvent(
                    entry.EntryId, entry.Description, entry.Value, entry.IsPaid, 
                    entry.Observations, entry.DateToPay, entry.BankAccountId, 
                    entry.BankAccount.Name, entry.EntryCategoryId, entry.EntryCategory.Name);

                await _mediator.PublishEvent(userRegisteredEvent);
            }

            return CommandResult.Ok(entry);
        }
    }
}
