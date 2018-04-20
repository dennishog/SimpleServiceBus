using System;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Events.Interfaces;
using Microsoft.Azure.EventHubs.Processor;

namespace DS.SimpleServiceBus.EventHubs.Services
{
    internal class AzureEventHubProcessorFactory : IEventProcessorFactory
    {
        private readonly CancellationToken _cancellationToken;
        private readonly Func<IEventMessage, CancellationToken, Task> _onMessage;

        public AzureEventHubProcessorFactory(Func<IEventMessage, CancellationToken, Task> onMessage,
            CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _onMessage = onMessage;
        }

        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            var processor = new AzureEventHubProcessor(_onMessage, _cancellationToken);
            return processor;
        }
    }
}