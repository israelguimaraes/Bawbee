using Bawbee.Domain.Core.Models;

namespace Bawbee.Domain.Entities
{
    public class EntryCategory : BaseEntity
    {
        public int EntryCategoryId { get; private set; }
        public string Name { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        public EntryCategory(string name, int userId)
        {
            Name = name;
            UserId = userId;
        }
    }
}
