using System;

namespace DS.SimpleServiceBus.Events.Interfaces
{
    /// <summary>
    /// </summary>
    public interface IEvent
    {
        Guid EventId { get; }
    }
}