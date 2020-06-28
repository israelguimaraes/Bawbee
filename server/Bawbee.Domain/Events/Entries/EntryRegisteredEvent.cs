using Bawbee.Domain.Core.Events;
using System;

namespace Bawbee.Domain.Events.Entries
{
    public class EntryRegisteredEvent : Event
    {
        public int EntryId { get; }
        public string Description { get; }
        public decimal Value { get; }
        public bool IsPaid { get; }
        public string Observations { get; }
        public DateTime DateToPay { get; }
        public int BankAccountId { get; }
        public int EntryCategoryId { get; }

        public EntryRegisteredEvent(
            int entryId, string description, decimal value, bool isPaid, 
            string observations, DateTime dateToPay, int bankAccountId, int entryCategoryId)
        {
            EntryId = entryId;
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
