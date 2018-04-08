namespace DS.SimpleServiceBus.Events.Interfaces
{
    public interface IEventMessage
    {
        byte[] Event { get; set; }
    }
}