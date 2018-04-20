using DS.SimpleServiceBus.EventHubs.Configuration.Interfaces;

namespace DS.SimpleServiceBus.EventHubs.Configuration
{
    public class EventHubsEventServiceConfiguration : IEventHubsEventServiceConfiguration
    {
        public string EventQueueName { get; set; }
    }
}