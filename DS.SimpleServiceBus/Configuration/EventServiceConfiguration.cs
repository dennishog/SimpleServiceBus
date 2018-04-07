using DS.SimpleServiceBus.Configuration.Interfaces;

namespace DS.SimpleServiceBus.Configuration
{
    public class EventServiceConfiguration : IEventServiceConfiguration
    {
        public EventServiceConfiguration()
        {
            
        }

        public EventServiceConfiguration(string eventQueueName)
        {
            EventQueueName = eventQueueName;
        }

        public string EventQueueName { get; set; }
    }
}