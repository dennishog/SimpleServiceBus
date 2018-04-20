using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Services.Interfaces
{
    /// <summary>
    ///     Should only be implemented by EventService. Inherit abstract class EventService to build a new EventService.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        ///     Publishes an event
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishAsync(IEvent @event, CancellationToken cancellationToken);

        /// <summary>
        ///     Adds an EventHandler
        /// </summary>
        /// <typeparam name="T">Accepts a class implementing IEventHandler</typeparam>
        void RegisterEventHandler<T>() where T : IEventHandler;

        /// <summary>
        ///     Adds an EventHandler
        /// </summary>
        /// <param name="eventHandler">Instance of class implementing IEventHandler</param>
        void RegisterEventHandler(IEventHandler eventHandler);

        /// <summary>
        ///     When message is received from the broker, this method executes all EventHandlers
        ///     built to handle this type of Event.
        /// </summary>
        /// <param name="eventMessage">The message received from the broker</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        Task EventMessageReceived(IEventMessage eventMessage, CancellationToken cancellationToken);

        /// <summary>
        ///     Disconnects this service from the IServiceBus instance
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        Task StopAsync(CancellationToken cancellationToken);
    }
}