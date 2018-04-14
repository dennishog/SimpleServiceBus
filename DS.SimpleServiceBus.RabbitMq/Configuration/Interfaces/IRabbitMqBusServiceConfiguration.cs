using DS.SimpleServiceBus.Configuration.Interfaces;

namespace DS.SimpleServiceBus.RabbitMq.Configuration.Interfaces
{
    public interface IRabbitMqBusServiceConfiguration : IBusServiceConfiguration
    {
        string Uri { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}