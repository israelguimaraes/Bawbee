using Bawbee.Infra.CrossCutting.Extensions;
using System;

namespace Bawbee.Domain.AggregatesModel.Entries
{
    public class Income : Entry
    {
        protected Income() { }

        public Income(
            string description,
            decimal value,
            bool isPaid,
            string observations,
            DateTime date,
            int userId,
            int bankAccountId,
            int categoryId)
            : base(description, value.ToPositive(), isPaid, observations, date, userId, bankAccountId, categoryId)
        {

        }
    }
}
