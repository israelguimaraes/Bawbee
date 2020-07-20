using System;

namespace Bawbee.Application.Query.Users.ReadModels.Entries
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
    }
}
