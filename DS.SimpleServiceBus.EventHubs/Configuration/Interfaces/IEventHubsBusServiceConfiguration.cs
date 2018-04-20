using DS.SimpleServiceBus.Configuration.Interfaces;

namespace DS.SimpleServiceBus.EventHubs.Configuration.Interfaces
{
    public interface IEventHubsBusServiceConfiguration : IBusServiceConfiguration
    {
        string EventHubConnectionString { get; set; }
        string EventHubName { get; set; }
        string ConsumerGroup { get; set; }
        string StorageAccountName { get; set; }
        string StorageConnectionString { get; set; }
    }
}