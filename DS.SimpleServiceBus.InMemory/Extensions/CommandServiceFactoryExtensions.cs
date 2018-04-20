using System;
using DS.SimpleServiceBus.Factories.Interfaces;
using DS.SimpleServiceBus.InMemory.Configuration;
using DS.SimpleServiceBus.InMemory.Configuration.Interfaces;
using DS.SimpleServiceBus.InMemory.Services;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.InMemory.Extensions
{
    public static class CommandServiceFactoryExtensions
    {
        public static ICommandService UsingInMemory(this ICommandServiceFactoryExtensionHook extensionHook,
            IBusService busService, Action<IInMemoryCommandServiceConfiguration> action)
        {
            IInMemoryCommandServiceConfiguration configuration = new InMemoryCommandServiceConfiguration();
            action(configuration);

            return new InMemoryCommandService(busService, configuration);
        }
    }
}