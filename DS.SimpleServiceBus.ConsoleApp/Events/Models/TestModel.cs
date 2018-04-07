using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.ConsoleApp.Events.Models
{
    public class TestModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}