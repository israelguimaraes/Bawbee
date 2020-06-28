using Bawbee.Domain.Core.Models;

namespace Bawbee.Domain.Entities
{
    public class User : BaseEntity
    {
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        // For Dapper
        protected User() { }

        protected User(int userId)
        {
            UserId = userId;
        }

        public User(string name, string lastName, string email, string password, int userId = default)
            : this(userId)
        {
            Name = name;
            LastName = lastName;
            Email = email.ToLower();
            Password = password;
        }
    }
}
