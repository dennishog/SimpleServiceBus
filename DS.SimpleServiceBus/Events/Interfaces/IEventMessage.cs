using System;

namespace DS.SimpleServiceBus.Events.Interfaces
{
    public interface IEventMessage
    {
        Guid MessageId { get; set; }
        byte[] Event { get; set; }
    }
}