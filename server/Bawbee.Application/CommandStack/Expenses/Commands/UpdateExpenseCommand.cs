using Bawbee.Core.Commands;
using Bawbee.Infra.CrossCutting.Extensions;
using FluentValidation;
using System;

namespace Bawbee.Application.CommandStack.Expenses.Commands
{
    public class UpdateExpenseCommand : BaseCommand
    {
        public int EntryId { get; }
        public int UserId { get; }
        public string Description { get; }
        public decimal Value { get; }
        public bool IsPaid { get; }
        public string Observations { get; }
        public DateTime DateToPay { get; }
        public int BankAccountId { get; }
        public int EntryCategoryId { get; }

        public UpdateExpenseCommand(
            int entryId, int userId, string description, decimal value,
            bool isPaid, string observations, DateTime dateToPay,
            int bankAccountId, int entryCategoryId)
        {
            EntryId = entryId;
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

            RuleFor(c => c.DateToPay)
               .Must(c => c != DateTime.MinValue)
               .WithMessage($"{nameof(CreateExpenseCommand.DateToPay)} is required");

            RuleFor(c => c.BankAccountId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(CreateExpenseCommand.BankAccountId)} is invalid");

            RuleFor(c => c.EntryCategoryId)
                .Must(c => c.IsGreaterThanZero())
                .WithMessage($"{nameof(CreateExpenseCommand.EntryCategoryId)} is invalid");
        }
    }
}
