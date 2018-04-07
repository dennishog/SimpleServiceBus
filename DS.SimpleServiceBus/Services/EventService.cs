﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Configuration;
using DS.SimpleServiceBus.Configuration.Interfaces;
using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Extensions;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.Services
{
    /// <summary>
    ///     Used for sending events and registering EventHandlers. The bus must be created and started before EventService is
    ///     instantiated.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IBusService _busService;
        private readonly IEventServiceConfiguration _configuration;
        private readonly ICollection<IEventHandler> _eventHandlers;

        /// <summary>
        ///     Connects a new ReceieveEndpoint to the BusService with queuename according to configuration
        /// </summary>
        /// <param name="busService">The BusService that this EventService should use</param>
        /// <param name="action">Configuration for the EventService</param>
        public EventService(IBusService busService, Action<IEventServiceConfiguration> action)
        {
            _busService = busService;
            _configuration = EventServiceConfigurator.Configure(action);

            busService.ConnectHandlerAsync(_configuration.EventQueueName, EventMessageReceived, CancellationToken.None)
                .Wait();

            _eventHandlers = new List<IEventHandler>();
        }

        /// <summary>
        ///     Publishes an event
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task PublishAsync(IEvent @event, CancellationToken cancellationToken)
        {
            var eventMessage = new EventMessage
            {
                MessageId = @event.EventId,
                Event = @event.ToBytes()
            };

            await _busService.PublishAsync(eventMessage, cancellationToken);
        }

        /// <summary>
        ///     Adds an EventHandler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RegisterEventHandler<T>() where T : IEventHandler
        {
            _eventHandlers.Add(Activator.CreateInstance<T>());
        }

        public void RegisterEventHandler(IEventHandler eventHandler)
        {
            _eventHandlers.Add(eventHandler);
        }

        public async Task EventMessageReceived(IEventMessage eventMessage, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Recieved event with id {eventMessage.MessageId}");

            var @event = eventMessage.Event.FromBytes();
            var listeners = _eventHandlers.Where(w => w.ForEvent == @event.EventId);

            foreach (var eventListener in listeners)
                await eventListener.ExecuteInternalAsync(@event, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busService.DisconnectHandlerAsync(_configuration.EventQueueName, cancellationToken);
        }
    }
}