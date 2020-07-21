using System.Collections.Generic;

namespace Bawbee.Mobile.Models.Dashboards
{
    public class Dashboard
    {
        public string UserName { get; set; }
        public decimal TotalCurrentBalance { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }
        public IEnumerable<MonthExpense> CurrentMonthExpenses { get; set; }
    }
}
