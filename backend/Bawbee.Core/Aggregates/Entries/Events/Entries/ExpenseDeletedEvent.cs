using System;

namespace Bawbee.Core.Aggregates.Entries.Events.Entries
{
    public class ExpenseDeletedEvent : BaseEntryEvent
    {
        public ExpenseDeletedEvent(
            int entryId, string description, decimal value,
            bool isPaid, string observations, DateTime date,
            int userId, int bankAccountId, int categoryId)
            : base(entryId, description, value, isPaid, observations, date, userId, bankAccountId, categoryId)
        {

        }
    }
}
