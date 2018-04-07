using DS.SimpleServiceBus.Commands.Interfaces;

namespace DS.SimpleServiceBus.ConsoleApp.Commands.Models.Response
{
    public class TestResponse2 : IResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}