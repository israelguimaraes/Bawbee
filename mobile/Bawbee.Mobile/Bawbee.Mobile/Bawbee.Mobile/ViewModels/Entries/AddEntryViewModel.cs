using Bawbee.Mobile.Models;
using Bawbee.Mobile.Models.Entries;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bawbee.Mobile.ViewModels.Entries
{
    public class AddEntryViewModel
    {
        public Expense Expense { get; set; }
        public List<EntryCategory> Categories { get; set; }
        public List<BankAccount> BankAccounts { get; set; }

        public AddEntryViewModel()
        {
            Expense = new Expense();

            Categories = new List<EntryCategory>
            {
                new EntryCategory { Id = 1, Name = "Home" },
                new EntryCategory { Id = 2, Name = "Food" },
                new EntryCategory { Id = 3, Name = "Grocery" },
            };

            BankAccounts = new List<BankAccount>
            {
                new BankAccount { Id = 1, Name = "ActivoBank" },
                new BankAccount { Id = 2, Name = "CTT" },
            };
        }

        public EntryCategory SelectedCategory { get; set; }
        public BankAccount SelectedBankAccount { get; set; }

        public ICommand AddEntryCommand
        {
            get
            {
                return new Command(() =>
                {
                    Expense.EntryCategoryId = SelectedCategory.Id;
                    Expense.BankAccountId = SelectedCategory.Id;
                });
            }
        }
    }
}
