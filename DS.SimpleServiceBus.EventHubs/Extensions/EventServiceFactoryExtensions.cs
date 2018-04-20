using System;
using DS.SimpleServiceBus.EventHubs.Configuration;
using DS.SimpleServiceBus.EventHubs.Configuration.Interfaces;
using DS.SimpleServiceBus.EventHubs.Services;
using DS.SimpleServiceBus.Factories.Interfaces;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.EventHubs.Extensions
{
    public static class EventServiceFactoryExtensions
    {
        public static IEventService UsingEventHubs(this IEventServiceFactoryExtensionHook extensionHook,
            IBusService busService, Action<IEventHubsEventServiceConfiguration> action)
        {
            IEventHubsEventServiceConfiguration configuration = new EventHubsEventServiceConfiguration();
            action(configuration);

            return new EventHubsEventService(busService, configuration);
        }
    }
}