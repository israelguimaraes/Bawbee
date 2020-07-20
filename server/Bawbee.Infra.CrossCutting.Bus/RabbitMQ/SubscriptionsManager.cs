using System;
using System.Collections.Generic;

namespace Bawbee.Infra.CrossCutting.Bus.RabbitMQ
{
    public class SubscriptionsManager
    {
        private static IDictionary<string, Type> _eventTypes = new Dictionary<string, Type>();

        public void AddSubscriptionIfNotExists(string eventName, Type eventType)
        {
            if (!_eventTypes.ContainsKey(eventName))
                _eventTypes.Add(eventName, eventType);
        }

        public Type GetSubscriptionType(string eventName)
        {
            if (!_eventTypes.ContainsKey(eventName))
                throw new KeyNotFoundException($"The event [{eventName}] was not found.");

            return _eventTypes[eventName];
        }
    }
}
