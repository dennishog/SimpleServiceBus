namespace DS.SimpleServiceBus.Configuration.Interfaces
{
    public interface IEventServiceConfiguration
    {
        string EventQueueName { get; set; }
    }
}