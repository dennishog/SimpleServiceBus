﻿using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Exceptions;
using DS.SimpleServiceBus.Services;
using DS.SimpleServiceBus.Services.Interfaces;
using DS.SimpleServiceBus.Tests.Fakes;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DS.SimpleServiceBus.Tests.Services
{
    public class CommandServiceTests
    {
        private readonly IBusService _busService;
        private readonly ICommandService _systemUnderTest;

        public CommandServiceTests()
        {
            _busService = Substitute.For<IBusService>();
            _systemUnderTest = new CommandService(_busService, q => q.CommandQueueName = "test");
        }

        [Fact]
        public async Task SendRequestSuccessful()
        {
            // Arrange
            var responseModel = ResponseModelFake.GetResponseModelFake();
            var requestModel = RequestModelFake.GetRequestModelFake();

            _busService.Request(Arg.Any<string>(), Arg.Any<ICommandMessage>(), Arg.Any<CancellationToken>())
                .Returns(CommandMessageFake.GetCommandMessageFake(requestModel, responseModel));
            
            // Act
            var response =
                await _systemUnderTest.SendRequestAsync<RequestModelFake, ResponseModelFake>(requestModel,
                    CancellationToken.None);

            // Assert
            await _busService.Received()
                .Request(Arg.Any<string>(), Arg.Any<ICommandMessage>(), Arg.Any<CancellationToken>());
            
            Assert.Equal(10, response.Id);
            Assert.Equal("Dennis Hogström", response.Name);
        }

        [Fact]
        public async Task SendRequestNotConnectedReceieveEndpoint()
        {
            // Arrange
            var responseModel = ResponseModelFake.GetResponseModelFake();
            var requestModel = RequestModelFake.GetRequestModelFake();

            _busService.Request(Arg.Any<string>(), Arg.Any<ICommandMessage>(), Arg.Any<CancellationToken>())
                .Throws(new ReceiveEndpointNotConnectedException());

            // Act
            var response =
                _systemUnderTest.SendRequestAsync<RequestModelFake, ResponseModelFake>(requestModel,
                    CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<ReceiveEndpointNotConnectedException>(async () =>
                await _systemUnderTest.SendRequestAsync<RequestModelFake, ResponseModelFake>(requestModel,
                    CancellationToken.None));
        }
    }
}