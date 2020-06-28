using Bawbee.Domain.Core.Models;
using System.Collections.Generic;

namespace Bawbee.Domain.Entities
{
    public class User : BaseEntity
    {
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public ICollection<BankAccount> BankAccounts { get; private set; }

        protected User()
        {
            BankAccounts = new List<BankAccount>();
        }

        protected User(int userId) : this()
        {
            UserId = userId;
        }

        public User(string name, string lastName, string email, string password, int userId = default)
            : this(userId)
        {
            Name = name;
            LastName = lastName;
            Email = email.ToLower();
            Password = password;
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
                user.BankAccounts.Add(BankAccount.CreateDefaultBankAccount(user.UserId));

                return user;
            }
        }
    }
}
