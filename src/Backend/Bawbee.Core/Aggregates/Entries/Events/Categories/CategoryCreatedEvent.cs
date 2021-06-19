using Bawbee.SharedKernel;

namespace Bawbee.Core.Aggregates.Entries.Events.Categories
{
    public class CategoryCreatedEvent : BaseEvent
    {
        public int CategoryId { get; }
        public string Name { get; }
        public int UserId { get; }

        public CategoryCreatedEvent(int categoryId, string name, int userId)
        {
            CategoryId = categoryId;
            Name = name;
            UserId = userId;
        }
    }
}
