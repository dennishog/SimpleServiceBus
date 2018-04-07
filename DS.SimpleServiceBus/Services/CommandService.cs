using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Configuration;
using DS.SimpleServiceBus.Configuration.Interfaces;
using DS.SimpleServiceBus.Extensions;
using DS.SimpleServiceBus.Services.Interfaces;
using MassTransit;

namespace DS.SimpleServiceBus.Services
{
    /// <summary>
    ///     Used for sending and receiving request/response and registering CommandHandlers. The bus must be created and
    ///     started before CommandService is instantiated.
    /// </summary>
    public class CommandService : ICommandService
    {
        private readonly IBusService _busService;
        private readonly ICollection<ICommandHandler> _commandHandlers;
        private readonly ICommandServiceConfiguration _configuration;

        /// <summary>
        ///     Connects a new ReceieveEndpoint to the BusService with queuename according to configuration
        /// </summary>
        /// <param name="busService"></param>
        /// <param name="action"></param>
        public CommandService(IBusService busService, Action<ICommandServiceConfiguration> action)
        {
            _busService = busService;
            _configuration = CommandServiceConfigurator.Configure(action);

            busService.CreateRequestClientAsync(_configuration.CommandQueueName,
                TimeSpan.FromSeconds(10), CancellationToken.None).Wait();

            busService.ConnectConsumerAsync(_configuration.CommandQueueName, this, CancellationToken.None).Wait();

            _commandHandlers = new List<ICommandHandler>();
        }

        public void RegisterCommandHandler<T>() where T : ICommandHandler
        {
            _commandHandlers.Add(Activator.CreateInstance<T>());
        }

        public void RegisterCommandHandler(ICommandHandler commandHandler)
        {
            _commandHandlers.Add(commandHandler);
        }

        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(TRequest requestModel,
            CancellationToken cancellationToken) where TRequest : IRequestModel where TResponse : IResponseModel
        {
            var response = await _busService.Request(_configuration.CommandQueueName, new CommandMessage
            {
                RequestData = requestModel.ToBytes(),
                ExpectedRequestType = typeof(TRequest),
                ExpectedResponseType = typeof(TResponse)
            }, cancellationToken);

            return response.ResponseData.GetResponse<TResponse>();
        }

        public async Task Consume(ConsumeContext<ICommandMessage> context)
        {
            // Throw error if more than one CommandHandler exists for the same combination of Request and Responsemodels.
            var handler = _commandHandlers.Single(x =>
                x.ExpectedRequestType == context.Message.ExpectedRequestType &&
                x.ExpectedResponseType == context.Message.ExpectedResponseType);

            var response =
                await handler.ExecuteInternalAsync(context.Message.RequestData.GetRequest(), CancellationToken.None);

            var commandMessage = new CommandMessage
            {
                ExpectedRequestType = context.Message.ExpectedRequestType,
                ExpectedResponseType = context.Message.ExpectedResponseType,
                RequestData = context.Message.RequestData,
                ResponseData = response.ToBytes()
            };

            await context.RespondAsync(commandMessage);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busService.DisconnectConsumerAsync(_configuration.CommandQueueName, cancellationToken);
        }
    }
}