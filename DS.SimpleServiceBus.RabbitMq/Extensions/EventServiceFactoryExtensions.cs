using System;
using DS.SimpleServiceBus.Factories.Interfaces;
using DS.SimpleServiceBus.RabbitMq.Configuration;
using DS.SimpleServiceBus.RabbitMq.Configuration.Interfaces;
using DS.SimpleServiceBus.RabbitMq.Services;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.RabbitMq.Extensions
{
    public static class EventServiceFactoryExtensions
    {
        public static IEventService UsingRabbitMq(this IEventServiceFactoryExtensionHook extensionHook,
            IBusService busService, Action<IRabbitMqEventServiceConfiguration> action)
        {
            IRabbitMqEventServiceConfiguration configuration = new RabbitMqEventServiceConfiguration();
            action(configuration);

            return new RabbitMqEventService(busService, configuration);
        }
    }
}