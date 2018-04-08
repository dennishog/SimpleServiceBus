using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Events
{
    public class EventMessage : IEventMessage
    {
        public byte[] Event { get; set; }
    }
}