using System;
using DS.SimpleServiceBus.EventHubs.Configuration;
using DS.SimpleServiceBus.EventHubs.Configuration.Interfaces;
using DS.SimpleServiceBus.EventHubs.Services;
using DS.SimpleServiceBus.Factories.Interfaces;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.EventHubs.Extensions
{
    public static class BusServiceFactoryExtensions
    {
        public static IBusService UsingEventHubs(this IBusServiceFactoryExtensionHook extensionHook,
            Action<IEventHubsBusServiceConfiguration> action)
        {
            IEventHubsBusServiceConfiguration configuration = new EventHubsBusServiceConfiguration();
            action(configuration);

            return new EventHubsBusService(configuration);
        }
    }
}