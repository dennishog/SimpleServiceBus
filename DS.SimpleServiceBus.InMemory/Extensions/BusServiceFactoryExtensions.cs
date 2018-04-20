using System;
using DS.SimpleServiceBus.Factories.Interfaces;
using DS.SimpleServiceBus.InMemory.Configuration;
using DS.SimpleServiceBus.InMemory.Configuration.Interfaces;
using DS.SimpleServiceBus.InMemory.Services;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.InMemory.Extensions
{
    public static class BusServiceFactoryExtensions
    {
        public static IBusService UsingInMemory(this IBusServiceFactoryExtensionHook extensionHook,
            Action<IInMemoryBusServiceConfiguration> action)
        {
            IInMemoryBusServiceConfiguration configuration = new InMemoryBusServiceConfiguration();
            action(configuration);

            return new InMemoryBusService(configuration);
        }
    }
}