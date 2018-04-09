using DS.SimpleServiceBus.Configuration.Interfaces;
using DS.SimpleServiceBus.Services;
using DS.SimpleServiceBus.Services.Interfaces;
using System;
using DS.SimpleServiceBus.Configuration;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace DS.SimpleServiceBus.Factories
{
    public static class BusServiceFactory
    {
        public static IBusService CreateUsingRabbitMq(Action<IRabbitMqBusServiceConfiguration> action)
        {
            var configuration = BusServiceConfigurator.GetRabbitMqConfig(action);
            IRabbitMqHost host = null;
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                host = cfg.Host(new Uri(configuration.Uri), h =>
                {
                    h.Username(configuration.Username);
                    h.Password(configuration.Password);
                });
            });

            return new BusService(bus, host);
        }

        //public static IBusService CreateUsingAzureServiceBus(Action<IAzureServiceBusBusServiceConfiguration> action)
        //{
        //    var configuration = BusServiceConfigurator.GetAzureServiceBusConfig(action);
        //    IRabbitMqHost host = null;
        //    var bus = Bus.Factory.CreateUsingAzure
        //}
    }
}
