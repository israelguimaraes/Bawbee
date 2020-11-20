using Bawbee.Core.Commands;
using System;

namespace Bawbee.Application.CommandStack.Expenses.Commands
{
    public abstract class EntryCommand : BaseCommand
    {
        public int EntryId { get; protected set; }
        public string Description { get; protected set; }
        public decimal Value { get; protected set; }
        public bool IsPaid { get; protected set; }
        public string Observations { get; protected set; }
        public DateTime DateToPay { get; protected set; }
        public int UserId { get; protected set; }
        public int BankAccountId { get; protected set; }
        public int CategoryId { get; protected set; }

        protected EntryCommand(
            string description, decimal value, bool isPaid,
            string observations, DateTime dateToPay, int userId,
            int bankAccountId, int categoryId, int entryId = default)
        {
            Description = description;
            Value = value;
            IsPaid = isPaid;
            Observations = observations;
            DateToPay = dateToPay;
            UserId = userId;
            BankAccountId = bankAccountId;
            CategoryId = categoryId;
            EntryId = entryId;
        }
    }
}
