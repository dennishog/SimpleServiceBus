using DS.SimpleServiceBus.Factories.Interfaces;

namespace DS.SimpleServiceBus.Factories
{
    public static class BusServiceFactory
    {
        public static IBusServiceFactoryExtensionHook Create { get; } =
            (IBusServiceFactoryExtensionHook) new BusServiceFactoryExtensionHook();
    }
}