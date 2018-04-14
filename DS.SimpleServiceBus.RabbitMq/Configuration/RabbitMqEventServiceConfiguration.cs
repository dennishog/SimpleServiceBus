using DS.SimpleServiceBus.RabbitMq.Configuration.Interfaces;

namespace DS.SimpleServiceBus.RabbitMq.Configuration
{
    public class RabbitMqEventServiceConfiguration : IRabbitMqEventServiceConfiguration
    {
        public RabbitMqEventServiceConfiguration()
        {
        }

        public RabbitMqEventServiceConfiguration(string eventQueueName)
        {
            EventQueueName = eventQueueName;
        }

        public string EventQueueName { get; set; }
    }
}