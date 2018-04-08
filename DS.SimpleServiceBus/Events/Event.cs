using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Events
{
    public abstract class Event<TModel> : IEvent
        where TModel : IModel
    {
        public TModel Model { get; set; }
    }
}