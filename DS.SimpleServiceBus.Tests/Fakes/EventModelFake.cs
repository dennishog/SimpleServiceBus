using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Tests.Fakes
{
    public class EventModelFake : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}