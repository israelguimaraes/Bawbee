using System;

namespace Bawbee.Mobile.ReadModels.Entries
{
    public class EntryReadModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string CategoryName { get; set; }
        public string BankAccountName { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CreatedAtFormatted { get => CreatedAt.ToString("dd/MM/yy"); }
        public string IsPaidFormatted { get => IsPaid ? "Paid" : "Unpaid"; }
    }
}
