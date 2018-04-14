using System;
using DS.SimpleServiceBus.Factories.Interfaces;
using DS.SimpleServiceBus.RabbitMq.Configuration;
using DS.SimpleServiceBus.RabbitMq.Configuration.Interfaces;
using DS.SimpleServiceBus.RabbitMq.Services;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.RabbitMq.Extensions
{
    public static class CommandServiceFactoryExtensions
    {
        public static ICommandService UsingRabbitMq(this ICommandServiceFactoryExtensionHook extensionHook,
            IBusService busService, Action<IRabbitMqCommandServiceConfiguration> action)
        {
            IRabbitMqCommandServiceConfiguration configuration = new RabbitMqCommandServiceConfiguration();
            action(configuration);

            return new RabbitMqCommandService(busService, configuration);
        }
    }
}