using Bawbee.Domain.Core.Events;

namespace Bawbee.Domain.Commands.Users.Events
{
    public class UserRegisteredEvent : Event
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public UserRegisteredEvent(int id, string name, string lastName, string email, string password)
        {
            Id = id;
            AggregateId = id;
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
        }
    }
}
