using Bawbee.Core.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;
using System;

namespace Bawbee.Application.CommandStack.Expenses.Commands
{
    public class CreateExpenseCommand : BaseCommand
    {
        public int UserId { get; }
        public int EntryId { get; }
        public string Description { get; }
        public decimal Value { get; }
        public bool? IsPaid { get; }
        public string Observations { get; }
        public DateTime DateToPay { get; }
        public int BankAccountId { get; }
        public int CategoryId { get; }

        public CreateExpenseCommand(
            int userId, string description, decimal value,
            bool isPaid, string observations, DateTime dateToPay,
            int bankAccountId, int categoryId)
        {
            UserId = userId;
            Description = description;
            Value = value;
            IsPaid = isPaid;
            Observations = observations;
            DateToPay = dateToPay;
            BankAccountId = bankAccountId;
            CategoryId = categoryId;
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
                .NotEmpty()
                .WithMessage($"{nameof(CreateExpenseCommand.Value)} is required");

            RuleFor(c => c.IsPaid)
               .NotEmpty()
               .WithMessage($"{nameof(CreateExpenseCommand.IsPaid)} is required");

            RuleFor(c => c.DateToPay)
               .Must(c => c != DateTime.MinValue)
               .WithMessage($"{nameof(CreateExpenseCommand.DateToPay)} is required");

            RuleFor(c => c.BankAccountId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(CreateExpenseCommand.BankAccountId)} is invalid");

            RuleFor(c => c.CategoryId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(CreateExpenseCommand.CategoryId)} is invalid");
        }
    }
}
