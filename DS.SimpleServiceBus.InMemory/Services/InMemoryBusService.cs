using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Exceptions;
using DS.SimpleServiceBus.InMemory.Configuration.Interfaces;
using DS.SimpleServiceBus.InMemory.Services.Interfaces;
using DS.SimpleServiceBus.Services;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.InMemory.Services
{
    public class InMemoryBusService : BusService, IInMemoryBusService
    {
        private Dictionary<string, ICommandService> _commandConsumers;
        private Dictionary<string, Func<IEventMessage, CancellationToken, Task>> _eventHandlers;
        private readonly IInMemoryBusServiceConfiguration _configuration;
        private readonly InMemoryEventProcessor _eventProcessor;

        public InMemoryBusService(IInMemoryBusServiceConfiguration configuration)
        {
            _configuration = configuration;
            _eventProcessor = new InMemoryEventProcessor(configuration.QueuePath);
            _eventProcessor.MessageRecieved += EventProcessorOnMessageRecieved; 
        }

        private void EventProcessorOnMessageRecieved(object sender, InMemoryMessageReceivedEventArgs inMemoryMessageReceivedEventArgs)
        {
            _eventHandlers[inMemoryMessageReceivedEventArgs.QueueName](inMemoryMessageReceivedEventArgs.Message,
                CancellationToken.None);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                _commandConsumers = new Dictionary<string, ICommandService>();
                _eventHandlers = new Dictionary<string, Func<IEventMessage, CancellationToken, Task>>();
            }, cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                _commandConsumers.Clear();
                _eventHandlers.Clear();
            }, cancellationToken);
        }

        public override async Task ConnectConsumerAsync(string queueName, ICommandService commandService,
            CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (!(commandService is InMemoryCommandService commandServiceCasted))
                    throw new Exception("Unsupported service");

                if (_commandConsumers.ContainsKey(queueName))
                    throw new Exception("Cant add second consumer with the same queueName");

                _commandConsumers.Add(queueName, commandServiceCasted);
            }, cancellationToken);
        }

        public override async Task DisconnectConsumerAsync(string queueName, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (!_commandConsumers.ContainsKey(queueName))
                    throw new Exception("No consumer exists with the supplied queueName");

                _commandConsumers.Remove(queueName);
            }, cancellationToken);
        }

        public override async Task ConnectHandlerAsync(string queueName,
            Func<IEventMessage, CancellationToken, Task> messageReceived, CancellationToken cancellationToken)
        {
            _eventHandlers.Add(queueName, messageReceived);
            await _eventProcessor.ConnectHandler(queueName, cancellationToken);
        }

        public override async Task DisconnectHandlerAsync(string queueName, CancellationToken cancellationToken)
        {
            await Task.Run(() => { _eventHandlers.Remove(queueName); }, cancellationToken);
        }

        public override async Task CreateRequestClientAsync(string queueName, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            await Task.Run(() => { }, cancellationToken);
        }

        public override async Task<ICommandMessage> Request(string queueName, ICommandMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                var handle = _commandConsumers.Single(x => x.Key == queueName).Value;
                return await ((InMemoryCommandService) handle).Consume(request);
            }
            catch (Exception)
            {
                throw new ReceiveEndpointNotConnectedException($"{queueName} has no connected ReceiveEndpoint");
            }
        }

        public override async Task PublishAsync<T>(T message, CancellationToken cancellationToken)
        {
            await _eventProcessor.Publish(message, cancellationToken);
        }
    }
}