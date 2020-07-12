using Bawbee.Mobile.Models;
using System.Collections.Generic;

namespace Bawbee.Mobile.ViewModels
{
    public class DashboardViewModel
    {
        public List<BankAccount> BankAccounts { get; set; }

        public DashboardViewModel()
        {
            BankAccounts = new List<BankAccount>();
            BankAccounts.Add(new BankAccount { Id = 1, Name = "ActivoBank", CurrentBalance = 291.17m });
            BankAccounts.Add(new BankAccount { Id = 2, Name = "CTT", CurrentBalance = 754.82m });
            //BankAccounts.Add(new BankAccount { Id = 3, Name = "Banco do Brasil", CurrentBalance = 55534.82m });
            //BankAccounts.Add(new BankAccount { Id = 4, Name = "Santander", CurrentBalance = 34.84m });
            //BankAccounts.Add(new BankAccount { Id = 5, Name = "BCP", CurrentBalance = 224.22m });
        }
    }
}
