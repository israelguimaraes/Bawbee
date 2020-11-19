using Bawbee.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bawbee.Domain.AggregatesModel.Users
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        [JsonConstructor]
        public Category(string name, int userId, int id = default)
        {
            Name = name;
            UserId = userId;
            Id = id;
        }

        public static IEnumerable<Category> GetDefaultCategoriesForNewUsers(int userId)
        {
            var list = new List<Category>
            {
                new Category("Home", userId),
                new Category("Food", userId),
                new Category("Grocery", userId),
                new Category("Leisure and Fun", userId),
                new Category("Transport", userId),
                new Category("Debts and Loans", userId),
                new Category("Shoppings", userId),
                new Category("Salary", userId),
                new Category("Investments", userId)
            };

            return list;
        }
    }
}
