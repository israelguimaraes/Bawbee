using Bawbee.Application.CommandStack.Entries.Validators;
using System;

namespace Bawbee.Application.CommandStack.Entries.Commands
{
    public class NewEntryCommand : Core.Commands.BaseCommand
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

        public NewEntryCommand(
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
            ValidationResult = new NewEntryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
