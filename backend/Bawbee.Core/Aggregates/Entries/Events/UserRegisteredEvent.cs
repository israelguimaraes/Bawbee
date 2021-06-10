using Bawbee.Core.Aggregates.Users;
using Bawbee.SharedKernel;
using System.Collections.Generic;

namespace Bawbee.Core.Aggregates.Entries.Events
{
    public class UserRegisteredEvent : BaseEvent
    {
        public int UserId { get; }
        public string Name { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Password { get; }
        public IEnumerable<BankAccount> BankAccounts { get; }
        public IEnumerable<Category> Categories { get; }

        public UserRegisteredEvent(
            int userId, string name, string lastName, string email, string password, 
            IEnumerable<BankAccount> bankAccounts, IEnumerable<Category> categories)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            BankAccounts = bankAccounts;
            Categories = categories;
            UserId = userId;
        }
    }
}
