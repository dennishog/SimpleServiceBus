using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DS.SimpleServiceBus.Services
{
    public abstract class BusService : IBusService
    {
        public abstract Task StartAsync(CancellationToken cancellationToken);

        public abstract Task StopAsync(CancellationToken cancellationToken);

        public abstract Task ConnectConsumerAsync(string queueName, ICommandService commandService,
            CancellationToken cancellationToken);

        public abstract Task DisconnectConsumerAsync(string queueName, CancellationToken cancellationToken);

        public abstract Task ConnectHandlerAsync(string queueName,
            Func<IEventMessage, CancellationToken, Task> messageReceived, CancellationToken cancellationToken);

        public abstract Task DisconnectHandlerAsync(string queueName, CancellationToken cancellationToken);

        public abstract Task CreateRequestClientAsync(string queueName, TimeSpan timeout,
            CancellationToken cancellationToken);

        public abstract Task<ICommandMessage> Request(string queueName, ICommandMessage request,
            CancellationToken cancellationToken);

        public abstract Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : EventMessage;
    }
}
