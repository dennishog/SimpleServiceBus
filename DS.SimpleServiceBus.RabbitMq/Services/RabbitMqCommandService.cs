using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Extensions;
using DS.SimpleServiceBus.RabbitMq.Configuration.Interfaces;
using DS.SimpleServiceBus.RabbitMq.Services.Interfaces;
using DS.SimpleServiceBus.Services;
using DS.SimpleServiceBus.Services.Interfaces;
using MassTransit;

namespace DS.SimpleServiceBus.RabbitMq.Services
{
    internal class RabbitMqCommandService : CommandService, IRabbitMqCommandService
    {
        public RabbitMqCommandService(IBusService busService, IRabbitMqCommandServiceConfiguration config) : base(
            busService,
            config)
        {
        }

        public async Task Consume(ConsumeContext<ICommandMessage> context)
        {
            // Throw error if more than one CommandHandler exists for the same combination of Request and Responsemodels.
            var handler = CommandHandlers.Single(x =>
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
    }
}