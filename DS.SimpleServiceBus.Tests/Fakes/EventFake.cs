using System;
using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Tests.Fakes
{
    public class EventFake : Event<EventModelFake>
    {
        public EventFake()
        {
            EventId = new Guid("7FFD3FCE-9E09-4C0B-B017-920BB831CDA2");
        }

        public static IEvent GetEventFake()
        {
            return new EventFake
            {
                Model = new EventModelFake
                {
                    Id = 10,
                    Name = "Dennis Hogström"
                }
            };
        }
    }
}