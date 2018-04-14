using DS.SimpleServiceBus.Factories.Interfaces;

namespace DS.SimpleServiceBus.Factories
{
    public static class CommandServiceFactory
    {
        public static ICommandServiceFactoryExtensionHook Create { get; } =
            (ICommandServiceFactoryExtensionHook) new CommandServiceFactoryExtensionHook();
    }
}