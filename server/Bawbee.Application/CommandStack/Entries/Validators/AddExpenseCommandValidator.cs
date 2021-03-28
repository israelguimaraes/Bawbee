using Bawbee.Application.CommandStack.Entries.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;
using System;

namespace Bawbee.Application.CommandStack.Entries.Validators
{
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
