using System;

namespace Bawbee.Application.Users.InputModels.Entries
{
    public class NewEntryInputModel
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public bool IsPaid { get; set; }
        public string Observations { get; set; }
        public DateTime DateToPay { get; set; }
        public int BankAccountId { get; set; }
    }
}
