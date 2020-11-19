using Bawbee.Core.Events;
using Bawbee.Domain.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Bawbee.Domain.Events
{
    //public class UserRegisteredEvent : Event
    //{
    //    public int UserId { get; private set; }
    //    public string Name { get; private set; }
    //    public string LastName { get; private set; }
    //    public string Email { get; private set; }
    //    public string Password { get; private set; }
    //    public IEnumerable<BankAccount> BankAccounts { get; private set; }
    //    public IEnumerable<EntryCategory> EntryCategories { get; private set; }

    //    public UserRegisteredEvent(int userId, string name, string lastName, string email, string password)
    //    {
    //        UserId = userId;
    //        Name = name;
    //        LastName = lastName;
    //        Email = email;
    //        Password = password;
    //    }

    //    [JsonConstructor]
    //    public UserRegisteredEvent(int userId, string name, string lastName, string email, string password, IEnumerable<BankAccount> bankAccounts, IEnumerable<EntryCategory> entryCategories)
    //    {
    //        UserId = userId;
    //        Name = name;
    //        LastName = lastName;
    //        Email = email;
    //        Password = password;
    //        BankAccounts = bankAccounts;
    //        EntryCategories = entryCategories;
    //    }
    //}

    public class UserRegisteredEvent : Event
    {
        public User User { get; private set; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
