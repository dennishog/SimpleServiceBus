using DS.SimpleServiceBus.RabbitMq.Configuration.Interfaces;

namespace DS.SimpleServiceBus.RabbitMq.Configuration
{
    public class RabbitMqCommandServiceConfiguration : IRabbitMqCommandServiceConfiguration
    {
        public RabbitMqCommandServiceConfiguration()
        {
        }

        public RabbitMqCommandServiceConfiguration(string commandQueueName)
        {
            CommandQueueName = commandQueueName;
        }

        public string CommandQueueName { get; set; }
    }
}