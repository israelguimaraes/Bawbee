using Bawbee.Domain.Core.Events;

namespace Bawbee.Domain.Events.EntryCategories
{
    public class EntryCategoryAddedEvent : Event
    {
        public int EntryCategoryId { get; }
        public string Name { get; }
        public int UserId { get; }

        public EntryCategoryAddedEvent(int entryCategoryId, string name, int userId)
        {
            EntryCategoryId = entryCategoryId;
            Name = name;
            UserId = userId;
        }
    }
}
