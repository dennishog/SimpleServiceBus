using System;
using System.Threading;
using System.Threading.Tasks;

namespace DS.SimpleServiceBus.ConsoleApp.Events.EventHandlers
{
    public class TestEventListener2 : SimpleServiceBus.Events.EventHandler<TestEvent2>
    {
        public override async Task ExecuteAsync(TestEvent2 @event, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("TestEvent2");
                Console.WriteLine($"Event med id {@event.Model.Id} och namn {@event.Model.Name}");

                // Display changes
                foreach (var modelPropertyChange in @event.Changes)
                {
                    Console.WriteLine($"Changed property {modelPropertyChange.PropertyName}, value before {modelPropertyChange.ValueBefore}, value after {modelPropertyChange.ValueAfter}");
                }
            }, cancellationToken);
        }
    }
}