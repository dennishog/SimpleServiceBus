using DS.SimpleServiceBus.EventHubs.Configuration.Interfaces;

namespace DS.SimpleServiceBus.EventHubs.Configuration
{
    public class EventHubsBusServiceConfiguration : IEventHubsBusServiceConfiguration
    {
        public string EventHubConnectionString { get; set; }
        public string EventHubName { get; set; }
        public string ConsumerGroup { get; set; }
        public string StorageAccountName { get; set; }
        public string StorageConnectionString { get; set; }

        public EventHubsBusServiceConfiguration(string eventHubConnectionString, string eventHubName, string consumerGroup, string storageAccountName, string storageConnectionString)
        {
            EventHubConnectionString = eventHubConnectionString;
            EventHubName = eventHubName;
            ConsumerGroup = consumerGroup;
            StorageAccountName = storageAccountName;
            StorageConnectionString = storageConnectionString;
        }

        public EventHubsBusServiceConfiguration()
        {
            
        }
    }
}
