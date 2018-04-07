using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands;
using DS.SimpleServiceBus.ConsoleApp.Commands.Models.Request;
using DS.SimpleServiceBus.ConsoleApp.Commands.Models.Response;

namespace DS.SimpleServiceBus.ConsoleApp.Commands.CommandHandlers
{
    public class TestCommandHandler2 : CommandHandler<TestRequest, TestResponse2>
    {
        public override async Task<TestResponse2> ExecuteAsync(TestRequest requestModel,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() => new TestResponse2
            {
                Id = 12,
                Name = "Dennis Andersson"
            }, cancellationToken);
        }
    }
}