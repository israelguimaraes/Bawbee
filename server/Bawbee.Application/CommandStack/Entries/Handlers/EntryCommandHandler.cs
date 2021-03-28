using Bawbee.Application.Adapters;
using Bawbee.Application.CommandStack.Entries.Commands;
using Bawbee.Core.Bus;
using Bawbee.Core.Commands;
using Bawbee.Core.Notifications;
using Bawbee.Core.UnitOfWork;
using Bawbee.Domain.AggregatesModel.Entries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.CommandStack.Entries.Handlers
{
    public class EntryCommandHandler : CommandHandler,
        ICommandHandler<CreateExpenseCommand>,
        ICommandHandler<UpdateExpenseCommand>,
        ICommandHandler<DeleteExpenseCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IEntryRepository _entryRepository;

        public EntryCommandHandler(
            IMediatorHandler mediator,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notificationHandler,
            IEntryRepository entryRepository) : base(mediator, unitOfWork, notificationHandler)
        {
            _mediator = mediator;
            _entryRepository = entryRepository;
        }

        public async Task<CommandResult> Handle(CreateExpenseCommand command, CancellationToken cancellationToken)
        {
            var expense = command.MapToDomain();

            await _entryRepository.Add(expense);

            if (await CommitTransaction())
            {
                var @event = expense.MapToExpenseCreatedEvent();
                await _mediator.PublishEvent(@event);
                return Ok(command);
            }

            return Error();
        }

        public async Task<CommandResult> Handle(UpdateExpenseCommand command, CancellationToken cancellationToken)
        {
            var expense = await _entryRepository.GetById(command.EntryId);

            if (!expense.IsBelongToTheUser(command.UserId))
            {
                // TODO: log
                await AddDomainNotification("Invalid operation.");
                return CommandResult.Error();
            }

            expense = command.MapToDomain();

            await _entryRepository.Update(expense);

            if (await CommitTransaction())
            {
                var @event = expense.MapToExpenseUpdatedEvent();
                await _mediator.PublishEvent(@event);
                return CommandResult.Ok();
            }

            return CommandResult.Error();
        }

        public async Task<CommandResult> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
        {
            var expense = await _entryRepository.GetById(command.EntryId);

            if (!expense.IsBelongToTheUser(command.UserId))
            {
                // TODO log
                await AddDomainNotification("Invalid operation.");
                return CommandResult.Error();
            }

            await _entryRepository.Delete(expense.Id);

            if (await CommitTransaction())
            {
                var @event = expense.MapToExpenseDeletedEvent();
                await _mediator.PublishEvent(@event);
                return CommandResult.Ok();
            }

            return CommandResult.Error();
        }
    }
}
