using DS.SimpleServiceBus.InMemory.Configuration.Interfaces;

namespace DS.SimpleServiceBus.InMemory.Configuration
{
    public class InMemoryEventServiceConfiguration : IInMemoryEventServiceConfiguration
    {
        public InMemoryEventServiceConfiguration()
        {
        }

        public InMemoryEventServiceConfiguration(string eventQueueName)
        {
            EventQueueName = eventQueueName;
        }

        public string EventQueueName { get; set; }
    }
}