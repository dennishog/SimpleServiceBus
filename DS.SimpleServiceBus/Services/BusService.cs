using System;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.Services
{
    /// <summary>
    ///     Abstract class used for implementing a busservice
    /// </summary>
    public abstract class BusService : IBusService
    {
        /// <inheritdoc />
        public abstract Task StartAsync(CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task StopAsync(CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task ConnectConsumerAsync(string queueName, ICommandService commandService,
            CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task DisconnectConsumerAsync(string queueName, CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task ConnectHandlerAsync(string queueName,
            Func<IEventMessage, CancellationToken, Task> messageReceived, CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task DisconnectHandlerAsync(string queueName, CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task CreateRequestClientAsync(string queueName, TimeSpan timeout,
            CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task<ICommandMessage> Request(string queueName, ICommandMessage request,
            CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : EventMessage;
    }
}