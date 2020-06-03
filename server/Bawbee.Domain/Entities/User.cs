using Bawbee.Domain.Core.Models;
using Dapper.Contrib.Extensions;

namespace Bawbee.Domain.Entities
{
    [Table("Users")]
    public class User : BaseEntity<int>
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        protected User() { } // For Dapper

        public User(string name, string lastName, string email, string password)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
        }
    }
}
