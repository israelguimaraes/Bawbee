namespace Bawbee.Application.Query.Users.ReadModels.Entries
{
    public class MonthExpenseReadModel
    {
        public string Category { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Percent { get; set; }
    }
}
