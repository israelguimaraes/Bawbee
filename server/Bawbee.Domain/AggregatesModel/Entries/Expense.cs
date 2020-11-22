using Bawbee.Infra.CrossCutting.Extensions;
using System;

namespace Bawbee.Domain.AggregatesModel.Entries
{
    public class Expense : Entry
    {
        protected Expense() { }

        public Expense(
            string description, 
            decimal value, 
            bool isPaid,
            string observations, 
            DateTime date, 
            int userId,
            int bankAccountId, 
            int categoryId,
            int entryId = default)
            : base(description, value.ToNegative(), isPaid, observations, date, userId, bankAccountId, categoryId, entryId)
        {

        }
    }
}
