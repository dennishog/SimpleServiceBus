using System;
using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Events
{
    public class EventMessage : IEventMessage
    {
        public Guid MessageId { get; set; }
        public byte[] Event { get; set; }
    }
}