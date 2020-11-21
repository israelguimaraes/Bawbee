using System;

namespace Bawbee.Infra.Data.Documents.Users
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        public string Name { get; set; }
        public decimal InitialBalance { get; set; }
        public DateTime CreatedAt { get; set; }

        public BankAccount()
        {
            CreatedAt = DateTime.Now;
        }
    }
}