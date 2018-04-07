using System.Threading.Tasks;
using Xunit;

namespace DS.SimpleServiceBus.Tests.Services
{
    public class EventServiceTests
    {
        [Fact]
        public async Task publishEvent()
        {
            //// Arrange
            //var responseModel = ResponseModelFake.GetResponseModelFake();
            //var requestModel = RequestModelFake.GetRequestModelFake();

            //var serviceBus = Substitute.For<IBusService>();
            //var commandHandler = Substitute.For<ICommandHandler>();

            //var requestClient = Substitute.For<IRequestResponseClient>();
            //requestClient.Request(Arg.Any<ICommandMessage>(), Arg.Any<CancellationToken>())
            //    .Returns(CommandMessageFake.GetCommandMessageFake(requestModel, responseModel));

            //serviceBus.CreateRequestClientAsync(Arg.Any<string>(), Arg.Any<TimeSpan>(),
            //    Arg.Any<CancellationToken>()).ReturnsForAnyArgs(requestClient);

            //commandHandler.ExecuteInternalAsync(Arg.Any<IRequestModel>(), CancellationToken.None)
            //    .Returns(responseModel);
            ////commandHandler.ExecuteAsync(Arg.Any<RequestModelFake>(), Arg.Any<CancellationToken>()).Returns(responseModel);

            //var commandService = new CommandService(serviceBus, new CommandServiceConfiguration("test"));
            //commandService.RegisterCommandHandler(commandHandler);

            //// Act
            //var response = await commandService.SendRequestAsync<RequestModelFake, ResponseModelFake>(requestModel, CancellationToken.None);

            //// Assert
            //Assert.Equal(10, response.Id);
            //Assert.Equal("Dennis Hogström", response.Name);
        }
    }
}