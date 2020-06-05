using Bawbee.Domain.Core.Models;

namespace Bawbee.Domain.Entities
{
    public class UserContext : BaseEntity
    {
        public int UserContextId { get; private set; }
        public string Name { get; private set; }
        public int UserId { get; private set; }

        public User User { get; private set; }

        public UserContext(string name, int userId)
        {
            Name = name;
            UserId = userId;
        }
    }
}
