using DS.SimpleServiceBus.Factories.Interfaces;

namespace DS.SimpleServiceBus.Factories
{
    public static class EventServiceFactory
    {
        public static IEventServiceFactoryExtensionHook Create { get; } =
            (IEventServiceFactoryExtensionHook) new EventServiceFactoryExtensionHook();
    }
}