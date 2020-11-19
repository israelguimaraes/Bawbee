namespace Bawbee.Application.QueryStack.Users.ReadModels.Expenses
{
    public class MonthExpenseReadModel
    {
        public string Category { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Percent { get; set; }
    }
}
