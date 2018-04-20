using DS.SimpleServiceBus.InMemory.Configuration.Interfaces;

namespace DS.SimpleServiceBus.InMemory.Configuration
{
    public class InMemoryCommandServiceConfiguration : IInMemoryCommandServiceConfiguration
    {
        public InMemoryCommandServiceConfiguration()
        {
        }

        public InMemoryCommandServiceConfiguration(string commandQueueName)
        {
            CommandQueueName = commandQueueName;
        }

        public string CommandQueueName { get; set; }
    }
}