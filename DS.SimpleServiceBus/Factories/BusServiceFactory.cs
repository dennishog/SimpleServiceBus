using DS.SimpleServiceBus.Factories.Interfaces;

namespace DS.SimpleServiceBus.Factories
{
    /// <summary>
    ///     Factory class for creating IBusService
    /// </summary>
    public static class BusServiceFactory
    {
        /// <summary>
        ///     Instance for accessing extension methods
        /// </summary>
        public static IBusServiceFactoryExtensionHook Create { get; } =
            new BusServiceFactoryExtensionHook() as IBusServiceFactoryExtensionHook;
    }
}