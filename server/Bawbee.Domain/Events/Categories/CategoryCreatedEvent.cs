using Bawbee.Core.Events;

namespace Bawbee.Domain.Events.Categories
{
    public class CategoryCreatedEvent : Event
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
