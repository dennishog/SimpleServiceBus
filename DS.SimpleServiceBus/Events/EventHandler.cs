using System;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Events
{
    public abstract class EventHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        public Guid ForEvent => Activator.CreateInstance<TEvent>().EventId;

        public async Task ExecuteInternalAsync(IEvent @event, CancellationToken cancellationToken)
        {
            await ExecuteAsync((TEvent) @event, cancellationToken);
        }

        public abstract Task ExecuteAsync(TEvent @event, CancellationToken cancellationToken);
    }
}