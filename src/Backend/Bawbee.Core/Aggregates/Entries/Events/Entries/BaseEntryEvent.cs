using Bawbee.SharedKernel;
using System;

namespace Bawbee.Core.Aggregates.Entries.Events.Entries
{
    public abstract class BaseEntryEvent : BaseEvent
    {
        public int EntryId { get; }
        public string Description { get; }
        public decimal Value { get; }
        public bool IsPaid { get; }
        public string Observations { get; }
        public DateTime Date { get; }
        public int UserId { get; }
        public int BankAccountId { get; }
        public int CategoryId { get; }

        public BaseEntryEvent(
            int entryId, string description, decimal value,
            bool isPaid, string observations, DateTime date,
            int userId, int bankAccountId, int categoryId)
        {
            EntryId = entryId;
            Description = description;
            Value = value;
            IsPaid = isPaid;
            Observations = observations;
            Date = date;
            UserId = userId;
            BankAccountId = bankAccountId;
            CategoryId = categoryId;
        }
    }
}
