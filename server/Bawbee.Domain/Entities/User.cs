using Bawbee.Domain.Core.Models;

namespace Bawbee.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Password { get; }
    }
}
