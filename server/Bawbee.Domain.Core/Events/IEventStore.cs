﻿using System.Threading.Tasks;

namespace Bawbee.Domain.Core.Events
{
    public interface IEventStore
    {
        Task Store<T>(T @event) where T : Event;
    }
}
