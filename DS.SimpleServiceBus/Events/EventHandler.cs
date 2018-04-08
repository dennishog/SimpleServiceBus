﻿using DS.SimpleServiceBus.Events.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace DS.SimpleServiceBus.Events
{
    public abstract class EventHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        public async Task ExecuteInternalAsync(IEvent @event, CancellationToken cancellationToken)
        {
            await ExecuteAsync((TEvent) @event, cancellationToken);
        }

        public abstract Task ExecuteAsync(TEvent @event, CancellationToken cancellationToken);
    }
}