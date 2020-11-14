using System;

namespace Bawbee.Domain.Core.Models
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Entity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
