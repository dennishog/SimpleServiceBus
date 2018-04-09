﻿using DS.SimpleServiceBus.Commands;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Exceptions;
using DS.SimpleServiceBus.Services.Interfaces;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DS.SimpleServiceBus.Services
{
    internal class BusService : IBusService, IDisposable
    {
        private readonly Dictionary<string, HostReceiveEndpointHandle> _handles;
        private readonly Dictionary<string, IRequestResponseClient> _requestResponseClients;
        private readonly IBusControl _bus;
        private readonly IRabbitMqHost _host;

        public BusService(IBusControl bus, IRabbitMqHost host)
        {
            
            _handles = new Dictionary<string, HostReceiveEndpointHandle>();
            _requestResponseClients = new Dictionary<string, IRequestResponseClient>();

            _bus = bus;
            _host = host;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bus.StopAsync(cancellationToken);
        }

        public async Task ConnectConsumerAsync(string queueName, ICommandService commandService,
            CancellationToken cancellationToken)
        {
            if (_handles.ContainsKey(queueName))
                throw new Exception("Cant add second consumer with the same queueName");

            var handle = _host.ConnectReceiveEndpoint(queueName,
                x => { x.Consumer(() => commandService); });

            await handle.Ready;

            _handles.Add(queueName, handle);
        }

        public async Task DisconnectConsumerAsync(string queueName, CancellationToken cancellationToken)
        {
            if (!_handles.ContainsKey(queueName))
                throw new Exception("No consumer exists with the supplied queueName");

            await _handles.Single(x => x.Key == queueName).Value.StopAsync(cancellationToken);

            _handles.Remove(queueName);
        }

        public async Task CreateRequestClientAsync(string queueName,
            TimeSpan timeout, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                var requestClient = _bus.CreateRequestClient<ICommandMessage, ICommandMessage>(
                    new Uri($"{_host.Settings.HostAddress.AbsoluteUri}/{queueName}"),
                    timeout);

                _requestResponseClients.Add(queueName, new RequestResponseClient(requestClient));
            }, cancellationToken);
        }

        public async Task<ICommandMessage> Request(string queueName, ICommandMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                var handle = _handles.Single(x => x.Key == queueName).Value;

                await handle.Ready;
            }
            catch (Exception)
            {
                throw new ReceiveEndpointNotConnectedException($"{queueName} has no connected ReceiveEndpoint");
            }

            return await _requestResponseClients.Single(x => x.Key == queueName).Value
                .Request(request, cancellationToken);
        }

        public async Task ConnectHandlerAsync(string queueName,
            Func<IEventMessage, CancellationToken, Task> messageReceived, CancellationToken cancellationToken)
        {
            if (_handles.ContainsKey(queueName))
                throw new Exception("Cant add second handler with the same queueName");

            var handle = _host.ConnectReceiveEndpoint(queueName,
                e =>
                {
                    e.Handler<IEventMessage>(async context =>
                        await messageReceived(context.Message, cancellationToken));
                });

            await handle.Ready;

            _handles.Add(queueName, handle);
        }

        public async Task DisconnectHandlerAsync(string queueName, CancellationToken cancellationToken)
        {
            if (!_handles.ContainsKey(queueName))
                throw new Exception("No handler exists with the supplied queueName");

            await _handles.Single(x => x.Key == queueName).Value.StopAsync(cancellationToken);

            _handles.Remove(queueName);
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken)
            where T : EventMessage
        {
            // Wait for all connected handlers to be ready before publishing any messages
            _handles.ToList().ForEach(async x => await x.Value.Ready);

            await _bus.Publish(message, cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Task.Run(async () =>
                {
                    // Stop all ReceiveEndpoints before stopping the bus
                    _handles.ToList().ForEach(async x => await x.Value.StopAsync(CancellationToken.None));

                    await _bus.StopAsync(TimeSpan.FromSeconds(10));
                });
        }
    }
}