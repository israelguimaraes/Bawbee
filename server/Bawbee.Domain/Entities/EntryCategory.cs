using Bawbee.Domain.Core.Models;

namespace Bawbee.Domain.Entities
{
    public class EntryCategory : BaseEntity
    {
        public int EntryCategoryId { get; private set; }
        public string Name { get; private set; }
        public int UserContextId { get; private set; }
        public UserContext UserContext { get; private set; }

        public EntryCategory(string name, int userContextId)
        {
            Name = name;
            UserContextId = userContextId;
        }
    }
}
