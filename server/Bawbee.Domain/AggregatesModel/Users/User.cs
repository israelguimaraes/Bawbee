using Bawbee.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Bawbee.Domain.AggregatesModel.Users
{
    public class User : Entity, IAggregateRoot
    {
        public const int PASSWORD_MIN_LENGTH = 6;
        public const int PASSWORD_MAX_LENGTH = 10;

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        // TODO: protect lists
        public List<BankAccount> BankAccounts { get; private set; }
        public List<Category> Categories { get; private set; }

        protected User()
        {
            BankAccounts = new List<BankAccount>();
            Categories = new List<Category>();
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
        public User(
            string name,
            string lastName,
            string email,
            string password,
            IEnumerable<BankAccount> bankAccounts,
            IEnumerable<Category> categories,
            int id = default) : this(name, lastName, email, password, id)
        {
            BankAccounts = bankAccounts?.ToList();          // TODO: extensions IsEmpty()
            Categories = categories?.ToList();
        }

        public void AddNewBankAccount(BankAccount bankAccount)
        {
            if (BankAccounts.Any(b => b.Id == bankAccount.Id))
                return;

            BankAccounts.Add(bankAccount);
        }

        public void AddNewCategory(Category category)
        {
            if (Categories.Any(e => e.Id == category.Id))
                return;

            Categories.Add(category);
        }

        public Category GetCategoryById(int categoryId)
        {
            return Categories.FirstOrDefault(e => e.Id == categoryId);
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

                var defaultCategories = Category.GetDefaultCategoriesForNewUsers(user.Id);
                user.Categories.AddRange(defaultCategories);

                return user;
            }
        }
    }
}
