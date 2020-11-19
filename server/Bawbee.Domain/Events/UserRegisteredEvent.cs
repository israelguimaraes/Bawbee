using Bawbee.Core.Events;
using Bawbee.Domain.AggregatesModel.Users;

namespace Bawbee.Domain.Events
{
    public class UserRegisteredEvent : Event
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
