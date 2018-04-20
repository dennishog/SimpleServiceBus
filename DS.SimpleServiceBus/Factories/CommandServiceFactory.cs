using DS.SimpleServiceBus.Factories.Interfaces;

namespace DS.SimpleServiceBus.Factories
{
    /// <summary>
    ///     Factory class for creating ICommandService
    /// </summary>
    public static class CommandServiceFactory
    {
        /// <summary>
        ///     Instance for accessing extension methods
        /// </summary>
        public static ICommandServiceFactoryExtensionHook Create { get; } =
            new CommandServiceFactoryExtensionHook() as ICommandServiceFactoryExtensionHook;
    }
}