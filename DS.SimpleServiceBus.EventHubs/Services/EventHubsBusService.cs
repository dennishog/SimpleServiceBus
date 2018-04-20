using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.EventHubs.Configuration.Interfaces;
using DS.SimpleServiceBus.EventHubs.Services.Interfaces;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Services;
using DS.SimpleServiceBus.Services.Interfaces;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace DS.SimpleServiceBus.EventHubs.Services
{
    public class EventHubsBusService : BusService, IEventHubsBusService
    {
        private readonly IEventHubsBusServiceConfiguration _configuration;
        private readonly Dictionary<string, AzureEventHubProcessorFactory> _eventProcessors;
        private EventProcessorHost _eventHost;
        private Lazy<EventHubClient> _eventHubClient;

        public EventHubsBusService(IEventHubsBusServiceConfiguration configuration)
        {
            _configuration = configuration;

            if (
                string.IsNullOrWhiteSpace(configuration.EventHubName) ||
                string.IsNullOrWhiteSpace(configuration.ConsumerGroup) ||
                string.IsNullOrWhiteSpace(configuration.EventHubConnectionString) ||
                string.IsNullOrWhiteSpace(configuration.StorageConnectionString)
            )
                throw new ArgumentException("Mandatory configuration for event hub missing");

            _eventProcessors = new Dictionary<string, AzureEventHubProcessorFactory>();
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                var eventProcessorHostName = Guid.NewGuid().ToString();

                _eventHost = new EventProcessorHost(
                    eventProcessorHostName,
                    _configuration.EventHubName,
                    _configuration.ConsumerGroup,
                    _configuration.EventHubConnectionString,
                    _configuration.StorageConnectionString,
                    _configuration.StorageAccountName);

                var connectionStringBuilder =
                    new EventHubsConnectionStringBuilder(_configuration.EventHubConnectionString)
                    {
                        EntityPath = _configuration.EventHubName
                    };
                _eventHubClient = new Lazy<EventHubClient>(() =>
                    EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString()));
            }, cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _eventHost.UnregisterEventProcessorAsync();
            await _eventHubClient.Value.CloseAsync();
        }

        /// <summary>
        ///     Not used for eventhubs
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="commandService"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task ConnectConsumerAsync(string queueName, ICommandService commandService,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Not used for eventhubs
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task DisconnectConsumerAsync(string queueName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task ConnectHandlerAsync(string queueName,
            Func<IEventMessage, CancellationToken, Task> messageReceived, CancellationToken cancellationToken)
        {
            _eventProcessors.Add(queueName, new AzureEventHubProcessorFactory(messageReceived, cancellationToken));

            await _eventHost.RegisterEventProcessorFactoryAsync(
                new AzureEventHubProcessorFactory(messageReceived, cancellationToken),
                EventProcessorOptions.DefaultOptions);
        }

        public override async Task DisconnectHandlerAsync(string queueName, CancellationToken cancellationToken)
        {
            await Task.Run(() => { _eventProcessors.Remove(queueName); }, cancellationToken);
        }

        /// <summary>
        ///     Not used for eventhubs
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task CreateRequestClientAsync(string queueName, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Not used for eventhubs
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<ICommandMessage> Request(string queueName, ICommandMessage request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task PublishAsync<T>(T message, CancellationToken cancellationToken)
        {
            await _eventHubClient.Value.SendAsync(new EventData(message.Event));
        }
    }
}