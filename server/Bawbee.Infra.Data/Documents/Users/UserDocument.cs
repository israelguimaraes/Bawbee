using Bawbee.Core.Models;
using Bawbee.Infra.Data.Documents.Users;
using System.Collections.Generic;

namespace Bawbee.Infra.Data.Documents
{
    public class UserDocument : Document
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<BankAccount> BankAccounts { get; set; }
        public List<Category> Categories { get; set; }

        public UserDocument()
        {
            BankAccounts = new List<BankAccount>();
            Categories = new List<Category>();
        }
    }
}
