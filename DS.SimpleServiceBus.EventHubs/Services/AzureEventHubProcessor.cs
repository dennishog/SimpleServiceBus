using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace DS.SimpleServiceBus.EventHubs.Services
{
    public class AzureEventHubProcessor : IEventProcessor
    {
        private readonly CancellationToken _cancellationToken;
        private readonly Func<IEventMessage, CancellationToken, Task> _onMessage;

        public AzureEventHubProcessor(Func<IEventMessage, CancellationToken, Task> onMessage,
            CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _onMessage = onMessage;
        }

        public async Task OpenAsync(PartitionContext context)
        {
            await Task.Run(() => { }, _cancellationToken);
        }

        public async Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            if (reason == CloseReason.Shutdown)
                await context.CheckpointAsync();
        }

        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            try
            {
                var tasks = new List<Task>();

                foreach (var eventData in messages)
                    tasks.Add(_onMessage(new EventMessage {Event = eventData.Body.Array}, _cancellationToken));

                Task.WaitAll(tasks.ToArray(), _cancellationToken);
                await context.CheckpointAsync();
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error in processing: " + exp.Message);
            }
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            throw new NotImplementedException();
        }
    }
}