using Bawbee.Application.Command.Entries.Validators;
using Bawbee.Domain.Core.Commands;
using System;

namespace Bawbee.Application.Command.Entries
{
    public class NewEntryCommand : BaseCommand
    {
        public int UserId { get; }
        public int EntryId { get; }
        public string Description { get; }
        public decimal Value { get; }
        public bool? IsPaid { get; }
        public string Observations { get; }
        public DateTime DateToPay { get; }
        public int BankAccountId { get; }

        public NewEntryCommand(int userId, string description, decimal value, bool isPaid, string observations, DateTime dateToPay, int bankAccountId)
        {
            UserId = userId;
            Description = description;
            Value = value;
            IsPaid = isPaid;
            Observations = observations;
            DateToPay = dateToPay;
            BankAccountId = bankAccountId;
        }

        public override bool IsValid()
        {
            ValidationResult = new NewEntryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
