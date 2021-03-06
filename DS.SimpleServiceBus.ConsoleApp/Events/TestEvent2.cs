﻿using System;
using DS.SimpleServiceBus.ConsoleApp.Events.Models;
using DS.SimpleServiceBus.Events;

namespace DS.SimpleServiceBus.ConsoleApp.Events
{
    public class TestEvent2 : Event<TestModel>
    {
        public TestEvent2() { }

        public TestEvent2(Func<TestModel> setModel) : base(setModel)
        {
        }
    }
}