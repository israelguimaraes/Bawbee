using Bawbee.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bawbee.Domain.AggregatesModel.Users
{
    public class User : Entity, IAggregateRoot
    {
        public static readonly int PASSWORD_MIN_LENGTH = 6;
        public static readonly int PASSWORD_MAX_LENGTH = 10;

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        private List<BankAccount> _bankAccounts;
        public IEnumerable<BankAccount> BankAccounts => _bankAccounts;

        public List<Category> _categories;
        public IEnumerable<Category> Categories => _categories;

        protected User()
        {
            _bankAccounts = new List<BankAccount>();
            _categories = new List<Category>();
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

        public User(
            string name,
            string lastName,
            string email,
            string password,
            IEnumerable<BankAccount> bankAccounts,
            IEnumerable<Category> categories,
            int id = default) : this(name, lastName, email, password, id)
        {
            SetBankAccounts(bankAccounts);
            SetCategories(categories);
        }

        public void AddNewBankAccount(BankAccount bankAccount)
        {
            if (BankAccounts.Any(b => b.Id == bankAccount.Id))
                return;

            _bankAccounts.Add(bankAccount);
        }

        public void AddNewCategory(Category category)
        {
            if (Categories.Any(e => e.Id == category.Id))
                return;

            _categories.Add(category);
        }

        public Category GetCategoryById(int categoryId)
        {
            return Categories.FirstOrDefault(e => e.Id == categoryId);
        }

        public BankAccount GetBankAccountById(int bankAccountId)
        {
            return BankAccounts.FirstOrDefault(b => b.Id == bankAccountId);
        }

        private void SetCategories(IEnumerable<Category> categories)
        {
            if (categories.Any())
                _categories.AddRange(categories);
        }

        private void SetBankAccounts(IEnumerable<BankAccount> bankAccounts)
        {
            if (_bankAccounts.Any())
                _bankAccounts.AddRange(bankAccounts);
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
                user.AddNewBankAccount(defaultBankAccount);

                var defaultCategories = Category.GetDefaultCategoriesForNewUsers(user.Id);
                user.SetCategories(defaultCategories);

                return user;
            }
        }
    }
}
