using Bawbee.Core.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;
using System;

namespace Bawbee.Application.CommandStack.Expenses.Commands
{
    public class AddExpenseCommand : BaseCommand
    {
        public int UserId { get; }
        public int EntryId { get; }
        public string Description { get; }
        public decimal Value { get; }
        public bool? IsPaid { get; }
        public string Observations { get; }
        public DateTime DateToPay { get; }
        public int BankAccountId { get; }
        public int EntryCategoryId { get; }

        public AddExpenseCommand(
            int userId, string description, decimal value,
            bool isPaid, string observations, DateTime dateToPay,
            int bankAccountId, int entryCategoryId)
        {
            UserId = userId;
            Description = description;
            Value = value;
            IsPaid = isPaid;
            Observations = observations;
            DateToPay = dateToPay;
            BankAccountId = bankAccountId;
            EntryCategoryId = entryCategoryId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddExpenseCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddExpenseCommandValidator : AbstractValidator<AddExpenseCommand>
    {
        public AddExpenseCommandValidator()
        {
            RuleFor(c => c.UserId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(AddExpenseCommand.UserId)} is invalid");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage($"{nameof(AddExpenseCommand.Description)} is required");

            RuleFor(c => c.Value)
                .NotEmpty()
                .WithMessage($"{nameof(AddExpenseCommand.Value)} is required");

            RuleFor(c => c.IsPaid)
               .NotEmpty()
               .WithMessage($"{nameof(AddExpenseCommand.IsPaid)} is required");

            RuleFor(c => c.DateToPay)
               .Must(c => c != DateTime.MinValue)
               .WithMessage($"{nameof(AddExpenseCommand.DateToPay)} is required");

            RuleFor(c => c.BankAccountId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(AddExpenseCommand.BankAccountId)} is invalid");

            RuleFor(c => c.EntryCategoryId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(AddExpenseCommand.EntryCategoryId)} is invalid");
        }
    }
}
