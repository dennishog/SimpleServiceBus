using DS.SimpleServiceBus.InMemory.Configuration.Interfaces;

namespace DS.SimpleServiceBus.InMemory.Configuration
{
    public class InMemoryBusServiceConfiguration : IInMemoryBusServiceConfiguration
    {
        public string QueuePath { get; set; }
    }
}