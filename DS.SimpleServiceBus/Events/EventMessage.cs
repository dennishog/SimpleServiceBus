using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Events
{
    /// <summary>
    ///     Internal messaging class, used when sending messages through the broker
    /// </summary>
    public class EventMessage : IEventMessage
    {
        /// <summary>
        ///     The event as a byte[]
        /// </summary>
        public byte[] Event { get; set; }
    }
}