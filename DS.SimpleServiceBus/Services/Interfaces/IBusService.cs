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
        /// <summary>
        ///     Starts the bus
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StartAsync(CancellationToken cancellationToken);

        /// <summary>
        ///     Stops the bus
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StopAsync(CancellationToken cancellationToken);

        /// <summary>
        ///     Connects a consumer used for request/response
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="commandService"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ConnectConsumerAsync(string queueName, ICommandService commandService,
            CancellationToken cancellationToken);

        /// <summary>
        ///     Disconnects a consumer used for request/response
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DisconnectConsumerAsync(string queueName, CancellationToken cancellationToken);

        /// <summary>
        ///     Connects an eventhandler used for receiving events
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="messageReceived"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ConnectHandlerAsync(string queueName, Func<IEventMessage, CancellationToken, Task> messageReceived,
            CancellationToken cancellationToken);

        /// <summary>
        ///     Disconnects an eventhandler used for receiving events
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DisconnectHandlerAsync(string queueName, CancellationToken cancellationToken);

        /// <summary>
        ///     Create a request client used for request/response
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CreateRequestClientAsync(string queueName,
            TimeSpan timeout, CancellationToken cancellationToken);

        /// <summary>
        ///     Sends a request
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ICommandMessage> Request(string queueName, ICommandMessage request,
            CancellationToken cancellationToken);

        /// <summary>
        ///     Publishes an event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : EventMessage;
    }
}