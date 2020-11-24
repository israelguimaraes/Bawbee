using System;

namespace Bawbee.Domain.Events.Entries
{
    public class ExpenseUpdatedEvent : EntryEvent
    {
        public ExpenseUpdatedEvent(
            int entryId, string description, decimal value,
            bool isPaid, string observations, DateTime date,
            int userId, int bankAccountId, int categoryId)
            : base(entryId, description, value, isPaid, observations, date, userId, bankAccountId, categoryId)
        {

        }
    }
}
