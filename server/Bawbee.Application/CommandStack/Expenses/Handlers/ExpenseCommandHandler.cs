using Bawbee.Application.CommandStack.Expenses.Commands;
using Bawbee.Core.Bus;
using Bawbee.Core.Commands;
using Bawbee.Core.Notifications;
using Bawbee.Core.UnitOfWork;
using Bawbee.Domain.AggregatesModel.Entries;
using Bawbee.Domain.Events.Entries;
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
            var entry = new Expense(
                command.Description, command.Value, command.IsPaid.Value, command.Observations,
                command.DateToPay, command.UserId, command.BankAccountId, command.CategoryId);

            await _entryRepository.Add(entry);

            if (await CommitTransaction())
            {
                var @event = new ExpenseCreatedEvent(
                    entry.Id, entry.Description, entry.Value,
                    entry.IsPaid, entry.Observations, entry.DateToPay,
                    entry.UserId, entry.BankAccountId, entry.CategoryId);

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

            expense.Update(
                description: command.Description,
                value: command.Value,
                isPaid: command.IsPaid,
                observations: command.Observations,
                dateToPay: command.DateToPay,
                bankAccountId: command.BankAccountId,
                entryCategoryId: command.EntryCategoryId);

            await _entryRepository.Update(expense);

            if (await CommitTransaction())
            {
                var @event = new ExpenseUpdatedEvent(
                    expense.Id, expense.Description, expense.Value,
                    expense.IsPaid, expense.Observations, expense.DateToPay,
                    expense.UserId, expense.BankAccountId, expense.CategoryId);

                await _mediator.PublishEvent(@event);
            }

            return CommandResult.Ok();
        }

        public async Task<CommandResult> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
        {
            var entry = await _entryRepository.GetById(command.EntryId);

            if (!entry.IsBelongToTheUser(command.UserId))
            {
                // TODO log
                await AddDomainNotification("Invalid operation.");
            }

            await _entryRepository.Delete(entry.Id);

            if (await CommitTransaction())
            {
                var @event = new ExpenseDeletedEvent(
                    entry.Id, entry.Description, entry.Value,
                    entry.IsPaid, entry.Observations, entry.DateToPay,
                    entry.UserId, entry.BankAccountId, entry.CategoryId);

                await _mediator.PublishEvent(@event);
            }

            return CommandResult.Ok();
        }
    }
}
