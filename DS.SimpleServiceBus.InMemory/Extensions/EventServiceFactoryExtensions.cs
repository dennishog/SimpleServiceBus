using System;
using DS.SimpleServiceBus.Factories.Interfaces;
using DS.SimpleServiceBus.InMemory.Configuration;
using DS.SimpleServiceBus.InMemory.Configuration.Interfaces;
using DS.SimpleServiceBus.InMemory.Services;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.InMemory.Extensions
{
    public static class EventServiceFactoryExtensions
    {
        public static IEventService UsingInMemory(this IEventServiceFactoryExtensionHook extensionHook,
            IBusService busService, Action<IInMemoryEventServiceConfiguration> action)
        {
            IInMemoryEventServiceConfiguration configuration = new InMemoryEventServiceConfiguration();
            action(configuration);

            return new InMemoryEventService(busService, configuration);
        }
    }
}