using System;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Services.Interfaces
{
    public interface IBusService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);

        Task ConnectConsumerAsync(string queueName, ICommandService commandService,
            CancellationToken cancellationToken);

        Task DisconnectConsumerAsync(string queueName, CancellationToken cancellationToken);

        Task ConnectHandlerAsync(string queueName, Func<IEventMessage, CancellationToken, Task> messageReceived,
            CancellationToken cancellationToken);

        Task DisconnectHandlerAsync(string queueName, CancellationToken cancellationToken);

        Task CreateRequestClientAsync(string queueName,
            TimeSpan timeout, CancellationToken cancellationToken);

        Task<ICommandMessage> Request(string queueName, ICommandMessage request,
            CancellationToken cancellationToken);

        Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : EventMessage;
    }
}