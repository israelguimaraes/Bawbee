using System;

namespace Bawbee.Application.CommandStack.Entries.InputModels
{
    public abstract class EntryInputModel
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public bool IsPaid { get; set; }
        public string Observations { get; set; }
        public DateTime Date { get; set; }
        public int BankAccountId { get; set; }
        public int CategoryId { get; set; }
    }
}
