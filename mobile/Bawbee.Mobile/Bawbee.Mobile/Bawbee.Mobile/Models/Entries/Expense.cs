using Bawbee.Mobile.Helpers.Extensions;
using System;

namespace Bawbee.Mobile.Models.Entries
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }
        public bool IsPaid { get; set; }
        public string Observations { get; set; }
        public DateTime? Date { get; set; }
        public int? BankAccountId { get; set; }
        public int? CategoryId { get; set; }

        public Expense()
        {
            Date = DateTime.Now.Date;
        }

        public bool IsValid()
        {
            return Description.IsNotEmpty() && 
                Value.HasValue && 
                Date.HasValue &&
                BankAccountId.HasValue && 
                CategoryId.HasValue;
        }
    }
}
