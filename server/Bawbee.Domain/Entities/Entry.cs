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

        public int BankAccountId { get; private set; }
        public BankAccount BankAccount { get; private set; }

        public int EntryCategoryId { get; private set; }
        public EntryCategory EntryCategory { get; private set; }

        public Entry(
            string description, decimal value, bool isPaid, 
            string observations, DateTime dateToPay, 
            int bankAccountId, int entryCategoryId)
        {
            Description = description;
            Value = value;
            IsPaid = isPaid;
            Observations = observations;
            DateToPay = dateToPay;
            BankAccountId = bankAccountId;
            EntryCategoryId = entryCategoryId;
        }
    }
}
