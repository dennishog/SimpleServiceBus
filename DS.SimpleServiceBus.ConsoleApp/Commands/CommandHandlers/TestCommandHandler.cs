using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands;
using DS.SimpleServiceBus.ConsoleApp.Commands.Models.Request;
using DS.SimpleServiceBus.ConsoleApp.Commands.Models.Response;

namespace DS.SimpleServiceBus.ConsoleApp.Commands.CommandHandlers
{
    public class TestCommandHandler : CommandHandler<TestRequest, TestResponse>
    {
        public override async Task<TestResponse> ExecuteAsync(TestRequest requestModel,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() => new TestResponse
            {
                Id = 10,
                Name = "Dennis Hogström"
            }, cancellationToken);
        }
    }
}