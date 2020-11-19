using Bawbee.Core.Events;

namespace Bawbee.Domain.Events
{
    public class UserLoggedEvent : Event
    {
        public int UserId { get; }
        public string Name { get; }
        public string Email { get; }

        public UserLoggedEvent(int userId, string name, string email)
        {
            UserId = userId;
            Name = name;
            Email = email.ToLower();
        }
    }
}
