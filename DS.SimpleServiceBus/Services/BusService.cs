﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Configuration;
using DS.SimpleServiceBus.Configuration.Interfaces;
using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Services.Interfaces;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace DS.SimpleServiceBus.Services
{
    public class BusService : IBusService, IDisposable
    {
        private readonly Dictionary<string, HostReceiveEndpointHandle> _handles;
        private readonly Dictionary<string, IRequestResponseClient> _requestResponseClients;
        private IBusControl _bus;
        private IRabbitMqHost _host;
        private readonly IBusServiceConfiguration _configuration;

        public BusService(Action<IBusServiceConfiguration> action)
        {
            _configuration = BusServiceConfigurator.Configure(action);

            _handles = new Dictionary<string, HostReceiveEndpointHandle>();
            _requestResponseClients = new Dictionary<string, IRequestResponseClient>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {            
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                _host = cfg.Host(new Uri(_configuration.Uri), h =>
                {
                    h.Username(_configuration.Username);
                    h.Password(_configuration.Password);
                });
            });

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
            if (_handles.ContainsKey(queueName))
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
            var handle = _handles.Single(x => x.Key == queueName);

            await handle.Value.Ready;

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
            if (_handles.ContainsKey(queueName))
                throw new Exception("No handler exists with the supplied queueName");

            await _handles.Single(x => x.Key == queueName).Value.StopAsync(cancellationToken);

            _handles.Remove(queueName);
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken)
            where T : EventMessage
        {
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
                _bus.Stop(TimeSpan.FromSeconds(10));
        }
    }
}