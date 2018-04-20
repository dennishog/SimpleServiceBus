using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Configuration.Interfaces;
using DS.SimpleServiceBus.Extensions;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.Services
{
    /// <summary>
    ///     Used for sending and receiving request/response and registering CommandHandlers. The bus must be created and
    ///     started before CommandService is instantiated.
    /// </summary>
    public abstract class CommandService : ICommandService
    {
        private readonly IBusService _busService;
        private readonly ICommandServiceConfiguration _configuration;

        /// <summary>
        ///     Connects a new ReceieveEndpoint to the BusService with queuename according to configuration
        /// </summary>
        /// <param name="busService"></param>
        /// <param name="action"></param>
        protected CommandService(IBusService busService, ICommandServiceConfiguration action)
        {
            _busService = busService;
            _configuration = action;

            busService.CreateRequestClientAsync(_configuration.CommandQueueName,
                TimeSpan.FromSeconds(10), CancellationToken.None).Wait();

            busService.ConnectConsumerAsync(_configuration.CommandQueueName, this, CancellationToken.None).Wait();

            CommandHandlers = new List<ICommandHandler>();
        }

        /// <summary>
        ///     Collection of registered CommandHandlers
        /// </summary>
        public ICollection<ICommandHandler> CommandHandlers { get; }

        /// <inheritdoc />
        public void RegisterCommandHandler<T>() where T : ICommandHandler
        {
            CommandHandlers.Add(Activator.CreateInstance<T>());
        }

        /// <inheritdoc />
        public void RegisterCommandHandler(ICommandHandler commandHandler)
        {
            CommandHandlers.Add(commandHandler);
        }

        /// <inheritdoc />
        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(TRequest requestModel,
            CancellationToken cancellationToken) where TRequest : IRequestModel where TResponse : IResponseModel
        {
            var response = await _busService.Request(_configuration.CommandQueueName, new CommandMessage
            {
                RequestData = requestModel.ToBytes(),
                ExpectedRequestType = typeof(TRequest),
                ExpectedResponseType = typeof(TResponse)
            }, cancellationToken);

            return (TResponse) response.ResponseData.GetResponse();
        }

        /// <inheritdoc />
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busService.DisconnectConsumerAsync(_configuration.CommandQueueName, cancellationToken);
        }
    }
}