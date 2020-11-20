using Bawbee.Core.Models;
using System;

namespace Bawbee.Infra.Data.Documents
{
    public class EntryDocument : Document
    {
        public string UserDocumentId { get; set; }
        public int UserId { get; set; }
        public int EntryId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public bool IsPaid { get; set; }
        public string Observations { get; set; }
        public DateTime DateToPay { get; set; }

        public int BankAccountId { get; set; }
        public string BankAccountName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
