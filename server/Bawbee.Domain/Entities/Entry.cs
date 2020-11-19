using Bawbee.Core.Models;
using Bawbee.Infra.CrossCutting.Extensions;
using System;

namespace Bawbee.Domain.Entities
{
    public class Entry : Entity
    {
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public bool IsPaid { get; private set; }
        public string Observations { get; private set; }
        public DateTime DateToPay { get; private set; }

        public int UserId { get; private set; }
        public User User { get; private set; }

        public int BankAccountId { get; private set; }
        public BankAccount BankAccount { get; private set; }

        public int EntryCategoryId { get; private set; }
        public EntryCategory EntryCategory { get; private set; }

        protected Entry() { }

        protected Entry(int id)
        {
            Id = id;
        }

        public Entry(
            string description, decimal value, bool isPaid,
            string observations, DateTime dateToPay, int userId,
            int bankAccountId, int entryCategoryId, int id = default) : this(id)
        {
            Description = description.Trim();
            Value = value;
            IsPaid = isPaid;
            Observations = observations.IsNotEmpty() ? observations.Trim() : null;
            DateToPay = dateToPay;
            UserId = userId;
            BankAccountId = bankAccountId;
            EntryCategoryId = entryCategoryId;
        }

        public bool IsBelongToTheUser(int userId)
        {
            return UserId == userId;
        }

        public void Update(
            string description = null, decimal? value = null, bool? isPaid = null,
            string observations = null, DateTime? dateToPay = null,
            int? bankAccountId = null, int? entryCategoryId = null)
        {
            if (description.IsNotEmpty())
                Description = description;

            if (value.HasValue)
                Value = value.Value;

            if (isPaid.HasValue)
                IsPaid = isPaid.Value;

            if (observations.IsNotEmpty())
                Observations = observations;
            
            if (dateToPay.HasValue)
                DateToPay = dateToPay.Value;

            if (bankAccountId.HasValue)
                BankAccountId = bankAccountId.Value;

            if (entryCategoryId.HasValue)
                EntryCategoryId = entryCategoryId.Value;
        }
    }
}
