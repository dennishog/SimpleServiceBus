using System;
using DS.SimpleServiceBus.ConsoleApp.Events.Models;
using DS.SimpleServiceBus.Events;

namespace DS.SimpleServiceBus.ConsoleApp.Events
{
    public class TestEvent : Event<TestModel>
    {
        public TestEvent()
        {
            EventId = new Guid("C350C45D-F166-4A72-9432-156CEAAA2340");
        }
    }
}