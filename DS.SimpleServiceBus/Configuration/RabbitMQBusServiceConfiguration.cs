using DS.SimpleServiceBus.Configuration.Interfaces;

namespace DS.SimpleServiceBus.Configuration
{
    public class RabbitMqBusServiceConfiguration : IRabbitMqBusServiceConfiguration
    {
        public RabbitMqBusServiceConfiguration()
        {
        }

        public RabbitMqBusServiceConfiguration(string uri, string username, string password)
        {
            Uri = uri;
            Username = username;
            Password = password;
        }

        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}