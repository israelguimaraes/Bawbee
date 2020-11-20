using Bawbee.Application.Adapters;
using Bawbee.Application.CommandStack.Expenses.Commands;
using Bawbee.Core.Bus;
using Bawbee.Core.Commands;
using Bawbee.Core.Notifications;
using Bawbee.Core.UnitOfWork;
using Bawbee.Domain.AggregatesModel.Entries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.CommandStack.Entries.Handlers
{
    public class ExpenseCommandHandler : BaseCommandHandler,
        ICommandHandler<CreateExpenseCommand>,
        ICommandHandler<UpdateExpenseCommand>,
        ICommandHandler<DeleteExpenseCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IEntryRepository _entryRepository;

        public ExpenseCommandHandler(
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
            var entry = command.MapToDomain();

            await _entryRepository.Add(entry);

            if (await CommitTransaction())
            {
                var @event = entry.MapToExpenseCreatedEvent();

                await _mediator.PublishEvent(@event);
            }

            return CommandResult.Ok(entry);
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
            }

            return CommandResult.Ok();
        }

        public async Task<CommandResult> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
        {
            var expense = await _entryRepository.GetById(command.EntryId);

            if (!expense.IsBelongToTheUser(command.UserId))
            {
                // TODO log
                await AddDomainNotification("Invalid operation.");
            }

            await _entryRepository.Delete(expense.Id);

            if (await CommitTransaction())
            {
                var @event = expense.MapToExpenseDeletedEvent();

                await _mediator.PublishEvent(@event);
            }

            return CommandResult.Ok();
        }
    }
}
