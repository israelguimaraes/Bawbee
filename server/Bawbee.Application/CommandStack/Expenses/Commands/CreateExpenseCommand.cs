using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;
using System;

namespace Bawbee.Application.CommandStack.Expenses.Commands
{
    public class CreateExpenseCommand : EntryCommand
    {
        public CreateExpenseCommand(
            string description,
            decimal value,
            bool isPaid,
            string observations,
            DateTime dateToPay,
            int userId,
            int bankAccountId,
            int categoryId,
            int entryId = 0)
            : base(description, value, isPaid, observations, dateToPay, userId, bankAccountId, categoryId, entryId)
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new AddExpenseCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
    {
        public AddExpenseCommandValidator()
        {
            RuleFor(c => c.UserId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(CreateExpenseCommand.UserId)} is invalid");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage($"{nameof(CreateExpenseCommand.Description)} is required");

            RuleFor(c => c.Value)
                .Must(c => c.IsNotZero())
                .WithMessage($"{nameof(CreateExpenseCommand.Value)} is required");

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
