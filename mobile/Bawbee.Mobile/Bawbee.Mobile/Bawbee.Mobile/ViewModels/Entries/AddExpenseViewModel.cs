﻿using Bawbee.Mobile.Models;
using Bawbee.Mobile.Models.Entries;
using Bawbee.Mobile.Services;
using Bawbee.Mobile.Services.Entries;
using Bawbee.Mobile.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Entries
{
    public class AddExpenseViewModel : BaseViewModel
    {
        public Expense Expense { get; set; }

        private readonly ExpenseService _expenseService;
        private readonly UserService _userService;

        public AddExpenseViewModel()
        {
            Expense = new Expense();

            _expenseService = new ExpenseService();
            _userService = new UserService();
        }

        public async Task Init()
        {
            BankAccounts = await _userService.GetBankAccounts();
            Categories = await _userService.GetCategories();
        }

        private Category _selectedCategory;
        public Category SelectedCategory 
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                Expense.CategoryId = _selectedCategory?.Id;
                OnPropertyChanged();
            }
        }

        private BankAccount _selectedBankAccount;
        public BankAccount SelectedBankAccount 
        {
            get => _selectedBankAccount;
            set
            {
                _selectedBankAccount = value;
                Expense.BankAccountId = _selectedBankAccount?.Id;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BankAccount> _bankAccounts;
        public ObservableCollection<BankAccount> BankAccounts
        {
            get => _bankAccounts;
            set
            {
                _bankAccounts = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                Expense.Date = _selectedDate;
                OnPropertyChanged();
            }
        }

        public ICommand AddExpenseCommand
        {
            get
            {
                return new Command(async () =>
                {
                    // TODO: FluentValidation?
                    if (Expense.IsValid())
                    {
                        IsBusy = true;

                        if (await _expenseService.Add(Expense))
                            MessagingCenter.Send(this, MessageKey.EntryAdded);
                    }
                    else
                    {
                        // TODO: send validations
                        MessagingCenter.Send(this, MessageKey.EntryFormInvalid);
                    }

                    IsBusy = false;
                });
            }
        }

        public class MessageKey
        {
            public const string EntryAdded = nameof(AddExpenseCommand);
            public const string EntryFormInvalid = nameof(EntryFormInvalid);
        }
    }
}
