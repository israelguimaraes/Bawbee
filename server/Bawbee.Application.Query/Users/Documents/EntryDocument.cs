using Bawbee.Application.Query.Base;
using System;

namespace Bawbee.Application.Query.Users.Documents
{
    public class EntryDocument : Document
    {
        public string UserDocumentId { get; set; }
        public int EntryId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public bool IsPaid { get; set; }
        public string Observations { get; set; }
        public DateTime DateToPay { get; set; }

        public int BankAccountId { get; set; }
        public string BankAccountName { get; set; }

        public int EntryCategoryId { get; set; }
        public string EntryCategoryName { get; set; }
    }
}
