﻿using Bawbee.Application.Command.Entries;
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
        ICommandHandler<NewEntryCommand>,
        ICommandHandler<UpdateEntryCommand>,
        ICommandHandler<DeleteEntryCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IEntryRepository _entryRepository;

        public EntryCommandHandler(
            IMediatorHandler mediator, 
            IUnitOfWork unitOfWork, 
            INotificationHandler<DomainNotification> notificationHandler,
            IUserRepository userRepository,
            IEntryRepository entryRepository) : base(mediator, unitOfWork, notificationHandler)
        {
            _mediator = mediator;
            _entryRepository = entryRepository;
        }

        public async Task<CommandResult> Handle(NewEntryCommand command, CancellationToken cancellationToken)
        {
            var entry = new Entry(
                command.Description, command.Value, command.IsPaid.Value, command.Observations, 
                command.DateToPay, command.UserId, command.BankAccountId, command.EntryCategoryId);

            await _entryRepository.Add(entry);

            if (await CommitTransaction())
            {
                var @event = new EntryAddedEvent(
                    entry.Id, entry.Description, entry.Value, 
                    entry.IsPaid, entry.Observations, entry.DateToPay, 
                    entry.UserId, entry.BankAccountId, entry.EntryCategoryId);

                await _mediator.PublishEvent(@event);
            }

            return CommandResult.Ok(entry);
        }

        public async Task<CommandResult> Handle(UpdateEntryCommand command, CancellationToken cancellationToken)
        {
            var entry = await _entryRepository.GetById(command.EntryId);

            if (!entry.IsBelongToTheUser(command.UserId))
            {
                // TODO: log
                await AddDomainNotification("Invalid operation.");
                return CommandResult.Error();
            }

            entry.Update(
                description: command.Description,
                value: command.Value,
                isPaid: command.IsPaid,
                observations: command.Observations,
                dateToPay: command.DateToPay,
                bankAccountId: command.BankAccountId,
                entryCategoryId: command.EntryCategoryId);

            await _entryRepository.Update(entry);

            if (await CommitTransaction())
            {
                var @event = new EntryUpdatedEvent(
                    entry.Id, entry.Description, entry.Value,
                    entry.IsPaid, entry.Observations, entry.DateToPay,
                    entry.UserId, entry.BankAccountId, entry.EntryCategoryId);

                await _mediator.PublishEvent(@event);
            }

            return CommandResult.Ok();
        }

        public async Task<CommandResult> Handle(DeleteEntryCommand command, CancellationToken cancellationToken)
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
                var @event = new EntryDeletedEvent(
                    entry.Id, entry.Description, entry.Value,
                    entry.IsPaid, entry.Observations, entry.DateToPay,
                    entry.UserId, entry.BankAccountId, entry.EntryCategoryId);

                await _mediator.PublishEvent(@event);
            }

            return CommandResult.Ok();
        }
    }
}
