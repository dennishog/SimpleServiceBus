using DS.SimpleServiceBus.Events.Interfaces;
using Newtonsoft.Json;
using System;
using System.Text;

namespace DS.SimpleServiceBus.Events
{
    [Serializable]
    public abstract class Event<TModel> : IEvent
        where TModel : IModel
    {
        public TModel Model { get; set; }
        public Guid EventId { get; set; }
    }
}