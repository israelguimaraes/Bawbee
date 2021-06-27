using Bawbee.SharedKernel.Events;

namespace Bawbee.Core.Aggregates.Users.Events
{
    public class UserLoggedEvent : BaseEvent
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
