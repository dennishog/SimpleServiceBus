using DS.SimpleServiceBus.Commands.Interfaces;

namespace DS.SimpleServiceBus.ConsoleApp.Commands.Models.Request
{
    public class TestRequest : IRequestModel
    {
        public int Id { get; set; }
    }
}