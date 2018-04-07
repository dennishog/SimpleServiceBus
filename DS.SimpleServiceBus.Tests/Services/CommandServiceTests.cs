using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Services;
using DS.SimpleServiceBus.Services.Interfaces;
using DS.SimpleServiceBus.Tests.Fakes;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DS.SimpleServiceBus.Tests.Services
{
    public class CommandServiceTests
    {
        [Fact]
        public async Task TestSendRequestWithValidCommandHandler()
        {
            // Arrange
            var responseModel = ResponseModelFake.GetResponseModelFake();
            var requestModel = RequestModelFake.GetRequestModelFake();

            var serviceBus = Substitute.For<IBusService>();
            var commandHandler = Substitute.For<ICommandHandler>();

            serviceBus.Request(Arg.Any<string>(), Arg.Any<ICommandMessage>(), Arg.Any<CancellationToken>())
                .Returns(CommandMessageFake.GetCommandMessageFake(requestModel, responseModel));

            commandHandler.ExecuteInternalAsync(Arg.Any<IRequestModel>(), CancellationToken.None)
                .Returns(responseModel);

            var commandService = new CommandService(serviceBus, q => q.CommandQueueName = "test");
            commandService.RegisterCommandHandler(commandHandler);

            // Act
            var response =
                await commandService.SendRequestAsync<RequestModelFake, ResponseModelFake>(requestModel,
                    CancellationToken.None);

            // Assert
            Assert.Equal(10, response.Id);
            Assert.Equal("Dennis Hogström", response.Name);
        }
    }
}