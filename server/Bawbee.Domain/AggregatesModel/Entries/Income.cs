using Bawbee.Infra.CrossCutting.Extensions;
using System;

namespace Bawbee.Domain.AggregatesModel.Entries
{
    public class Income : Entry
    {
        public Income(
            string description,
            decimal value,
            bool isPaid,
            string observations,
            DateTime dateToPay,
            int userId,
            int bankAccountId,
            int categoryId)
            : base(description, value.ToPositive(), isPaid, observations, dateToPay, userId, bankAccountId, categoryId)
        {

        }
    }
}
