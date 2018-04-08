using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Tests.Fakes
{
    public class EventFake : Event<EventModelFake>
    {
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