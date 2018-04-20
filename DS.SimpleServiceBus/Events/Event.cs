using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Events
{
    /// <inheritdoc />
    /// <summary>
    ///     Abstract class used for implementing an event.
    /// </summary>
    /// <typeparam name="TModel">Class implementing IModel</typeparam>
    public abstract class Event<TModel> : IEvent
        where TModel : IModel
    {
        /// <summary>
        ///     The model to use for this event
        /// </summary>
        public TModel Model { get; set; }
    }
}