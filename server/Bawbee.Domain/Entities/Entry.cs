using Bawbee.Domain.Core.Models;
using System;

namespace Bawbee.Domain.Entities
{
    public class Entry : BaseEntity<int>
    {
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public bool IsPaid { get; private set; }
        public string Observations { get; private set; }
        public DateTime DateToPaid { get; private set; }
        public int BankAccountId { get; private set; }
        public int UserContextId { get; private set; }

        public BankAccount BankAccount { get; private set; }
        public UserContext UserContext { get; private set; }

        public Entry(
            string description, decimal value, bool isPaid, 
            string observations, DateTime dateToPaid, 
            int bankAccountId, int userContextId)
        {
            Description = description;
            Value = value;
            IsPaid = isPaid;
            Observations = observations;
            DateToPaid = dateToPaid;
            BankAccountId = bankAccountId;
            UserContextId = userContextId;
        }
    }
}
