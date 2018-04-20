using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Configuration.Interfaces;
using DS.SimpleServiceBus.Extensions;
using DS.SimpleServiceBus.InMemory.Services.Interfaces;
using DS.SimpleServiceBus.Services;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.InMemory.Services
{
    public class InMemoryCommandService : CommandService, IInMemoryCommandService
    {
        public InMemoryCommandService(IBusService busService, ICommandServiceConfiguration action) : base(busService,
            action)
        {
        }

        public async Task<ICommandMessage> Consume(ICommandMessage message)
        {
            // Throw error if more than one CommandHandler exists for the same combination of Request and Responsemodels.
            var handler = CommandHandlers.Single(x =>
                x.ExpectedRequestType == message.ExpectedRequestType &&
                x.ExpectedResponseType == message.ExpectedResponseType);

            var response =
                await handler.ExecuteInternalAsync(message.RequestData.GetRequest(), CancellationToken.None);

            return new CommandMessage
            {
                ExpectedRequestType = message.ExpectedRequestType,
                ExpectedResponseType = message.ExpectedResponseType,
                RequestData = message.RequestData,
                ResponseData = response.ToBytes()
            };
        }
    }
}