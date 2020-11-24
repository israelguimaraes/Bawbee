using System;

namespace Bawbee.Application.QueryStack.Users.ReadModels.Expenses
{
    public class ExpenseReadModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string CategoryName { get; set; }
        public string BankAccountName { get; set; }
        public bool IsPaid { get; set; }
        public DateTime Date { get; set; }
    }
}
