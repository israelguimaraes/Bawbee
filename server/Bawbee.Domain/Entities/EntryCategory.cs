using Bawbee.Domain.Core.Models;
using System.Collections.Generic;

namespace Bawbee.Domain.Entities
{
    public class EntryCategory : BaseEntity
    {
        public int EntryCategoryId { get; private set; }
        public string Name { get; private set; }
        public int UserId { get; private set; }

        public EntryCategory(string name, int userId)
        {
            Name = name;
            UserId = userId;
        }

        public static IEnumerable<EntryCategory> GetDefaultCategoriesForNewUsers(int userId)
        {
            var list = new List<EntryCategory>
            {
                new EntryCategory("House", userId),
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
