using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Events
{
    /// <summary>
    ///     Abstract class for implementing an EventHandler for Event T
    /// </summary>
    /// <typeparam name="TEvent">Class implementing IEvent</typeparam>
    public abstract class EventHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        /// <summary>
        ///     When eventhandlers are executed they are executed through this method.
        /// </summary>
        /// <param name="event">Instance of class implementing IEvent</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task ExecuteInternalAsync(IEvent @event, CancellationToken cancellationToken)
        {
            await ExecuteAsync((TEvent) @event, cancellationToken);
        }

        /// <summary>
        ///     This is the method that needs to be implemented by the EventHandlers.
        ///     This is called from the ExecuteInternalAsync and this is to be able to provide
        ///     a method which already has the event of the correct type.
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task ExecuteAsync(TEvent @event, CancellationToken cancellationToken);
    }
}