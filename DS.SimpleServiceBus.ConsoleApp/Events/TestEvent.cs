using DS.SimpleServiceBus.ConsoleApp.Events.Models;
using DS.SimpleServiceBus.Events;
using System;

namespace DS.SimpleServiceBus.ConsoleApp.Events
{
    public class TestEvent : Event<TestModel>
    {
        public TestEvent() { }

        public TestEvent(Func<TestModel> setModel) : base(setModel)
        {
        }
    }
}