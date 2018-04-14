using DS.SimpleServiceBus.ConsoleApp.Events.Models;
using DS.SimpleServiceBus.Events;

namespace DS.SimpleServiceBus.ConsoleApp.Events
{
    public class TestEvent : Event<TestModel>
    {
    }
}