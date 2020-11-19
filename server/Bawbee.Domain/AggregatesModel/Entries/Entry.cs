using Bawbee.Core.Models;
using Bawbee.Domain.AggregatesModel.Users;
using Bawbee.Infra.CrossCutting.Extensions;
using System;

namespace Bawbee.Domain.AggregatesModel.Entries
{
    public abstract class Entry : Entity, IAggregateRoot
    {
        public string Description { get; protected set; }
        public decimal Value { get; protected set; }
        public bool IsPaid { get; protected set; }
        public string Observations { get; protected set; }
        public DateTime DateToPay { get; protected set; }

        public int UserId { get; protected set; }
        public User User { get; protected set; }

        public int BankAccountId { get; protected set; }
        public BankAccount BankAccount { get; protected set; }

        public int CategoryId { get; protected set; }
        public Category EntryCategory { get; protected set; }

        public Entry(
            string description, decimal value, bool isPaid,
            string observations, DateTime dateToPay, int userId,
            int bankAccountId, int categoryId)
        {
            Description = description.Trim();
            Value = value;
            IsPaid = isPaid;
            Observations = observations.IsNotEmpty() ? observations.Trim() : null;
            DateToPay = dateToPay;
            UserId = userId;
            BankAccountId = bankAccountId;
            CategoryId = categoryId;
        }

        public bool IsBelongToTheUser(int userId)
        {
            return UserId == userId;
        }
    }
}
