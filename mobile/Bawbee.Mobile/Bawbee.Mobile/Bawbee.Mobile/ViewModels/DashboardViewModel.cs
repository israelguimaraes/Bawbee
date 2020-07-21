using Bawbee.Mobile.Models;
using Bawbee.Mobile.Models.Dashboards;
using Bawbee.Mobile.Services;
using Bawbee.Mobile.Services.Entries;
using Bawbee.Mobile.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Bawbee.Mobile.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly ExpenseService _expenseService;
        private readonly UserService _userService;

        public DashboardViewModel()
        {
            _expenseService = new ExpenseService();
            _userService = new UserService();

            BankAccounts = new ObservableCollection<BankAccount>();
            CurrentMonthExpenses = new ObservableCollection<MonthExpense>();
        }

        public async Task Init()
        {
            IsBusy = true;

            BankAccounts = await _userService.GetBankAccounts();
            CurrentMonthExpenses = await _expenseService.GetCurrentMonthExpenses();

            IsBusy = false;
        }

        private ObservableCollection<BankAccount> _bankAccounts;
        public ObservableCollection<BankAccount> BankAccounts
        {
            get => _bankAccounts;
            set
            {
                _bankAccounts = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SizeBankAccountsFrame));
            }
        }

        private ObservableCollection<MonthExpense> _currentMonthExpenses;
        public ObservableCollection<MonthExpense> CurrentMonthExpenses
        {
            get => _currentMonthExpenses;
            set
            {
                _currentMonthExpenses = value;
                OnPropertyChanged();
            }
        }

        public int SizeBankAccountsFrame => BankAccounts.Count >= 3 ? 150 : BankAccounts.Count * 60;
        public string CurrentMonthExpensesTitle => $"Current expenses ({DateTime.Now:MMMM})";
    }
}

