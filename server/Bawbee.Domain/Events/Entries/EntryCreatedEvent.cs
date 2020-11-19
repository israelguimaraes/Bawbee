using Bawbee.Core.Events;
using System;

namespace Bawbee.Domain.Events.Entries
{
    public class EntryCreatedEvent : Event
    {
        public int EntryId { get; }
        public string Description { get; }
        public decimal Value { get; }
        public bool IsPaid { get; }
        public string Observations { get; }
        public DateTime DateToPay { get; }
        public int UserId { get; }
        public int BankAccountId { get; }
        public int CategoryId { get; }

        public EntryCreatedEvent(
            int entryId, string description, decimal value,
            bool isPaid, string observations, DateTime dateToPay,
            int userId, int bankAccountId, int categoryId)
        {
            EntryId = entryId;
            Description = description;
            Value = value;
            IsPaid = isPaid;
            Observations = observations;
            DateToPay = dateToPay;
            UserId = userId;
            BankAccountId = bankAccountId;
            CategoryId = categoryId;
        }
    }
}
