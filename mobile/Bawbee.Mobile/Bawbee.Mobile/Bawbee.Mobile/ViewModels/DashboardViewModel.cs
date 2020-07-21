using Bawbee.Mobile.Models;
using Bawbee.Mobile.Models.Dashboards;
using Bawbee.Mobile.Services;
using Bawbee.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Bawbee.Mobile.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly DashboardService _dashboardService;

        public DashboardViewModel()
        {
            _dashboardService = new DashboardService();

            GetBankAccountsFake();
            GetCurrentMonthExpensesFake();
        }

        public async Task Init()
        {
            IsBusy = true;

            BankAccounts = await _dashboardService.GetBankAccounts();
            CurrentMonthExpenses = await _dashboardService.GetCurrentMonthExpenses();

            IsBusy = false;
        }

        public ObservableCollection<BankAccount> BankAccounts { get; set; }
        public ObservableCollection<MonthExpense> CurrentMonthExpenses { get; set; }

        private void GetBankAccountsFake()
        {
            BankAccounts = new List<BankAccount>();
            BankAccounts.Add(new BankAccount { Id = 1, Name = "ActivoBank", CurrentBalance = 291.17m });
            BankAccounts.Add(new BankAccount { Id = 2, Name = "CTT", CurrentBalance = 754.82m });
            BankAccounts.Add(new BankAccount { Id = 3, Name = "Banco do Brasil", CurrentBalance = 55534.82m });
            BankAccounts.Add(new BankAccount { Id = 4, Name = "Santander", CurrentBalance = 34.84m });
            BankAccounts.Add(new BankAccount { Id = 5, Name = "BCP", CurrentBalance = 224.22m });
        }

        private void GetCurrentMonthExpensesFake()
        {
            CurrentMonthExpenses = new List<MonthExpense>();
            CurrentMonthExpenses.Add(new MonthExpense { Category = "House", TotalValue = 793.19m, Percent = 60.76 });
            CurrentMonthExpenses.Add(new MonthExpense { Category = "Food", TotalValue = 291.33m, Percent = 20.31 });
            CurrentMonthExpenses.Add(new MonthExpense { Category = "Grocery", TotalValue = 155.45m, Percent = 10.20 });
            CurrentMonthExpenses.Add(new MonthExpense { Category = "Leisure and Fun", TotalValue = 175.45m, Percent = 10.20 });
            CurrentMonthExpenses.Add(new MonthExpense { Category = "Transport", TotalValue = 98.01m, Percent = 8.29 });
            CurrentMonthExpenses.Add(new MonthExpense { Category = "Shoppings", TotalValue = 85.63m, Percent = 6.07 });
            CurrentMonthExpenses.Add(new MonthExpense { Category = "Debts and Loans", TotalValue = 65.63m, Percent = 5.07 });
            CurrentMonthExpenses.Add(new MonthExpense { Category = "Investments", TotalValue = 45.63m, Percent = 4.07 });
        }

        public int SizeBankAccountsFrame => BankAccounts.Count >= 3 ? 150 : BankAccounts.Count * 60;
        public string CurrentMonthExpensesTitle => $"Current expenses ({DateTime.Now:MMMM})";
    }
}

