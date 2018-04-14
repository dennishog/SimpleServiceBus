using System;
using DS.SimpleServiceBus.Factories.Interfaces;
using DS.SimpleServiceBus.RabbitMq.Configuration;
using DS.SimpleServiceBus.RabbitMq.Configuration.Interfaces;
using DS.SimpleServiceBus.RabbitMq.Services;
using DS.SimpleServiceBus.Services.Interfaces;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace DS.SimpleServiceBus.RabbitMq.Extensions
{
    public static class BusServiceFactoryExtensions
    {
        public static IBusService UsingRabbitMq(this IBusServiceFactoryExtensionHook extensionHook,
            Action<IRabbitMqBusServiceConfiguration> action)
        {
            IRabbitMqBusServiceConfiguration configuration = new RabbitMqBusServiceConfiguration();
            action(configuration);

            IRabbitMqHost host = null;
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                host = cfg.Host(new Uri(configuration.Uri), h =>
                {
                    h.Username(configuration.Username);
                    h.Password(configuration.Password);
                });
            });

            return new RabbitMqBusService(bus, host);
        }
    }
}