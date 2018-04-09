using System;
using DS.SimpleServiceBus.Configuration.Interfaces;

namespace DS.SimpleServiceBus.Configuration
{
    public class BusServiceConfigurator
    {
        public static IRabbitMqBusServiceConfiguration GetRabbitMqConfig(Action<IRabbitMqBusServiceConfiguration> bsc)
        {
            IRabbitMqBusServiceConfiguration config = new RabbitMqBusServiceConfiguration();
            bsc(config);
            return config;
        }

        public static IAzureServiceBusBusServiceConfiguration GetAzureServiceBusConfig(Action<IAzureServiceBusBusServiceConfiguration> bsc)
        {
            IAzureServiceBusBusServiceConfiguration config = new AzureServiceBusBusServiceConfiguration();
            bsc(config);
            return config;
        }
    }
}