using Bawbee.Core.Aggregates.Entries.Events;
using Bawbee.Core.Aggregates.Users;
using System.Collections.Generic;

namespace Bawbee.Application.UseCases.Adapters
{
    public static class UserAdapter
    {
        public static UserRegisteredEvent MapToUserRegisteredEvent(this User user)
        {
            var bankAccounts = new List<BankAccount>();
            foreach (var b in user.BankAccounts)
            {
                bankAccounts.Add(new BankAccount(b.Name, b.InitialBalance, b.UserId, b.Id));
            }

            var categories = new List<Category>();
            foreach (var c in user.Categories)
            {
                categories.Add(new Category(c.Name, c.UserId, c.Id));
            }

            return new UserRegisteredEvent(
                user.Id, user.Name, user.LastName, user.Email,
                user.Password, bankAccounts, categories);
        }
    }
}
