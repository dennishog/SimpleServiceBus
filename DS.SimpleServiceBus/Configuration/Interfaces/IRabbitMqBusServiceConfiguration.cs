namespace DS.SimpleServiceBus.Configuration.Interfaces
{
    public interface IRabbitMqBusServiceConfiguration : IBusServiceConfiguration
    {
        string Uri { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}