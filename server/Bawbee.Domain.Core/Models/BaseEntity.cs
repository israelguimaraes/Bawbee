using System;

namespace Bawbee.Domain.Core.Models
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// string Id (for RavenDB)
        /// </summary>
        public string Id { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
