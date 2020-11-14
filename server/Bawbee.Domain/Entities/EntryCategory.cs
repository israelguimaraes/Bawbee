using Bawbee.Domain.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bawbee.Domain.Entities
{
    public class EntryCategory : Entity
    {
        public string Name { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        [JsonConstructor]
        public EntryCategory(string name, int userId, int id = default)
        {
            Name = name;
            UserId = userId;
            Id = id;
        }

        public static IEnumerable<EntryCategory> GetDefaultCategoriesForNewUsers(int userId)
        {
            var list = new List<EntryCategory>
            {
                new EntryCategory("Home", userId),
                new EntryCategory("Food", userId),
                new EntryCategory("Grocery", userId),
                new EntryCategory("Leisure and Fun", userId),
                new EntryCategory("Transport", userId),
                new EntryCategory("Debts and Loans", userId),
                new EntryCategory("Shoppings", userId),
                new EntryCategory("Salary", userId),
                new EntryCategory("Investments", userId)
            };

            return list;
        }
    }
}
