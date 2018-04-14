using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands.Interfaces;
using MassTransit;

namespace DS.SimpleServiceBus.RabbitMq.Commands
{
    public class RequestResponseClient : IRequestResponseClient
    {
        private readonly IRequestClient<ICommandMessage, ICommandMessage> _requestClient;

        public RequestResponseClient(IRequestClient<ICommandMessage, ICommandMessage> requestClient)
        {
            _requestClient = requestClient;
        }

        public async Task<ICommandMessage> Request(ICommandMessage request, CancellationToken cancellationToken)
        {
            return await _requestClient.Request(request, cancellationToken);
        }
    }
}