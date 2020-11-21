using Bawbee.Core.Models;
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

        public List<BankAccountDocument> BankAccounts { get; set; }
        public List<CategoryDocument> EntryCategories { get; set; }

        public UserDocument()
        {
            BankAccounts = new List<BankAccountDocument>();
            EntryCategories = new List<CategoryDocument>();
        }
    }
}
