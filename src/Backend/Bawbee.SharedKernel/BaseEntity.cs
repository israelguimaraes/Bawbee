using Bawbee.SharedKernel.Events;
using System;
using System.Collections.Generic;

namespace Bawbee.SharedKernel
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        private readonly List<BaseDomainEvent> _events;
        public IReadOnlyCollection<BaseDomainEvent> Events => _events;

        protected BaseEntity()
        {
            CreatedAt = DateTime.Now;
            _events = new List<BaseDomainEvent>();
        }

        public void AddEvent(BaseDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }
    }
}
