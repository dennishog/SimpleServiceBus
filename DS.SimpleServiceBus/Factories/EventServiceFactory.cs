using DS.SimpleServiceBus.Factories.Interfaces;

namespace DS.SimpleServiceBus.Factories
{
    /// <summary>
    ///     Factory class for creating IEventService
    /// </summary>
    public static class EventServiceFactory
    {
        /// <summary>
        ///     Instance for accessing extension methods
        /// </summary>
        public static IEventServiceFactoryExtensionHook Create { get; } =
            new EventServiceFactoryExtensionHook() as IEventServiceFactoryExtensionHook;
    }
}