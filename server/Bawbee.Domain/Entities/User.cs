using Bawbee.Domain.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Bawbee.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        // TODO: protect lists
        public List<BankAccount> BankAccounts { get; private set; }
        public List<EntryCategory> EntryCategories { get; private set; }

        protected User()
        {
            BankAccounts = new List<BankAccount>();
            EntryCategories = new List<EntryCategory>();
        }

        protected User(int id) : this()
        {
            Id = id;
        }

        public User(string name, string lastName, string email, string password, int id = default)
            : this(id)
        {
            Name = name;
            LastName = lastName;
            Email = email.ToLower();
            Password = password;
        }
        
        [JsonConstructor]
        public User(string name, string lastName, string email, string password, IEnumerable<BankAccount> bankAccounts, IEnumerable<EntryCategory> entryCategories, int id = default)
            : this(name, lastName, email, password, id)
        {
            BankAccounts = bankAccounts?.ToList();          // TODO: extensions IsEmpty()
            EntryCategories = entryCategories?.ToList();
        }

        public void AddNewBankAccount(BankAccount bankAccount)
        {
            if (BankAccounts.Any(b => b.Id == bankAccount.Id))
                return;

            BankAccounts.Add(bankAccount);
        }

        public void AddNewEntryCategory(EntryCategory entryCategory)
        {
            if (EntryCategories.Any(e => e.Id == entryCategory.Id))
                return;

            EntryCategories.Add(entryCategory);
        }

        public EntryCategory GetEntryCategoryById(int entryCategoryId)
        {
            return EntryCategories.FirstOrDefault(e => e.Id == entryCategoryId);
        }

        public BankAccount GetBankAccountById(int bankAccountId)
        {
            return BankAccounts.FirstOrDefault(b => b.Id == bankAccountId);
        }

        public abstract class UserFactory
        {
            public static User CreateNewPlataformUser(string name, string lastName, string email, string password)
            {
                var user = new User();
                user.Name = name;
                user.LastName = lastName;
                user.Email = email;
                user.Password = password;

                var defaultBankAccount = BankAccount.CreateDefaultBankAccount(user.Id);
                user.BankAccounts.Add(defaultBankAccount);

                var defaultCategories = EntryCategory.GetDefaultCategoriesForNewUsers(user.Id);
                user.EntryCategories.AddRange(defaultCategories);

                return user;
            }
        }
    }
}
