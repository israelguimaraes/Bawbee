using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;
using System;

namespace Bawbee.Application.CommandStack.Entries.Commands
{
    public class UpdateExpenseCommand : EntryCommand
    {
        public UpdateExpenseCommand(
            int entryId,
            string description,
            decimal value,
            bool isPaid,
            string observations,
            DateTime dateToPay,
            int userId,
            int bankAccountId,
            int categoryId)
            : base(description, value, isPaid, observations, dateToPay, userId, bankAccountId, categoryId, entryId)
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateExpenseCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommand>
    {
        public UpdateExpenseCommandValidator()
        {
            RuleFor(c => c.EntryId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(CreateExpenseCommand.EntryId)} is invalid");

            RuleFor(c => c.UserId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(CreateExpenseCommand.UserId)} is invalid");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage($"{nameof(CreateExpenseCommand.Description)} is required");

            RuleFor(c => c.Value)
                .NotEmpty()
                .WithMessage($"{nameof(CreateExpenseCommand.Value)} is required");

            RuleFor(c => c.IsPaid)
               .NotEmpty()
               .WithMessage($"{nameof(CreateExpenseCommand.IsPaid)} is required");

            RuleFor(c => c.Date)
               .Must(c => c != DateTime.MinValue)
               .WithMessage($"{nameof(CreateExpenseCommand.Date)} is required");

            RuleFor(c => c.BankAccountId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(CreateExpenseCommand.BankAccountId)} is invalid");

            RuleFor(c => c.CategoryId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(CreateExpenseCommand.CategoryId)} is invalid");
        }
    }
}
