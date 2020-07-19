using System;

namespace Bawbee.Application.Query.Users.Documents
{
    public class BankAccountDocument
    {
        public int BankAccountId { get; set; }
        public string Name { get; set; }
        public decimal InitialBalance { get; set; }
        public DateTime CreatedAt { get; set; }

        public BankAccountDocument()
        {
            CreatedAt = DateTime.Now;
        }
    }
}