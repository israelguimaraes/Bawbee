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
            INotificationHandler<DomainNotification> notificationHandler,
            IUserRepository userRepository) : base(mediator, unitOfWork, notificationHandler)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<CommandResult> Handle(NewEntryCommand command, CancellationToken cancellationToken)
        {
            var entry = new Entry(
                command.Description, command.Value, command.IsPaid.Value, command.Observations, 
                command.DateToPay, command.UserId, command.BankAccountId, command.EntryCategoryId);

            await _userRepository.AddNewEntry(entry);

            if (await CommitTransaction())
            {
                var userRegisteredEvent = new EntryAddedEvent(
                    entry.Id, entry.Description, entry.Value, 
                    entry.IsPaid, entry.Observations, entry.DateToPay, 
                    entry.UserId, entry.BankAccountId, entry.EntryCategoryId);

                await _mediator.PublishEvent(userRegisteredEvent);
            }

            return CommandResult.Ok(entry);
        }
    }
}
