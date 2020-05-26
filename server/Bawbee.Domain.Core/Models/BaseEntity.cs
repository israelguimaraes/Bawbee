using System;

namespace Bawbee.Domain.Core.Models
{
    public abstract class BaseEntity<TIdentity>
    {
        public TIdentity Id { get; protected set; }
        public DateTime CreatedAt { get; private set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
