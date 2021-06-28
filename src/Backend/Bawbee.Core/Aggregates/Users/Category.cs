using Bawbee.SharedKernel;
using System.Collections.Generic;

namespace Bawbee.Core.Aggregates.Users
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        protected Category() { }

        public Category(string name, int userId)
        {
            Name = name;
            UserId = userId;
        }

        public Category(string name, int userId, int id) : this(name, userId)
        {
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
