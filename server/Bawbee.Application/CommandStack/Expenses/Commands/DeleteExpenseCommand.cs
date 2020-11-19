﻿using Bawbee.Core.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;

namespace Bawbee.Application.CommandStack.Expenses.Commands
{
    public class DeleteExpenseCommand : BaseCommand
    {
        public int EntryId { get; }
        public int UserId { get; }

        public DeleteExpenseCommand(int entryId, int userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteExpenseCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DeleteExpenseCommandValidator : AbstractValidator<DeleteExpenseCommand>
    {
        public DeleteExpenseCommandValidator()
        {
            RuleFor(c => c.EntryId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(AddExpenseCommand.EntryId)} is invalid");

            RuleFor(c => c.UserId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(AddExpenseCommand.UserId)} is invalid");
        }
    }
}