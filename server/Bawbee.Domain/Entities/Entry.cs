using Bawbee.Domain.Core.Models;
using System;

namespace Bawbee.Domain.Entities
{
    public class Entry : BaseEntity
    {
        public int EntryId { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public bool IsPaid { get; private set; }
        public string Observations { get; private set; }
        public DateTime DateToPay { get; private set; }

        public int UserId { get; private set; }
        public User User { get; private set; }

        public int BankAccountId { get; private set; }
        public BankAccount BankAccount { get; private set; }

        public int EntryCategoryId { get; private set; }
        public EntryCategory EntryCategory { get; private set; }

        protected Entry(int entryId)
        {
            EntryId = entryId;
        }

        public Entry(
            string description, decimal value, bool isPaid,
            string observations, DateTime dateToPay, int userId,
            int bankAccountId, int entryCategoryId, int entryId = default) : this(entryId)
        {
            Description = description.Trim();
            Value = value;
            IsPaid = isPaid;
            Observations = observations?.Trim();
            DateToPay = dateToPay;
            UserId = userId;
            BankAccountId = bankAccountId;
            EntryCategoryId = entryCategoryId;
        }
    }
}
