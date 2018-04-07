using DS.SimpleServiceBus.ConsoleApp.Events.Models;
using DS.SimpleServiceBus.Events;
using System;

namespace DS.SimpleServiceBus.ConsoleApp.Events
{
    public class TestEvent2 : Event<TestModel>
    {
        public TestEvent2()
        {
            EventId = new Guid("C350C35D-F166-4A72-9432-156CEAAA2340");
        }
    }
}