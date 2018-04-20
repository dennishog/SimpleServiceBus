using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.EventHubs.Extensions;
using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Extensions;
using DS.SimpleServiceBus.Factories;
using DS.SimpleServiceBus.Services.Interfaces;
using DS.SimpleServiceBus.Tests.Fakes;
using NSubstitute;
using Xunit;

namespace DS.SimpleServiceBus.Tests.EventHubs.Services
{
    public class EventServiceTests
    {
        public EventServiceTests()
        {
            _busService = Substitute.For<IBusService>();
            _eventHandler = Substitute.For<EventHandler<EventFake>>();
            _systemUnderTest = EventServiceFactory.Create.UsingEventHubs(_busService, q => q.EventQueueName = "test");
        }

        private readonly IBusService _busService;
        private readonly IEventHandler _eventHandler;
        private readonly IEventService _systemUnderTest;

        [Fact]
        public async Task PublishSuccessful()
        {
            // Arrange
            var @event = EventFake.GetEventFake();

            _busService.When(b => b.PublishAsync(Arg.Any<EventMessage>(), Arg.Any<CancellationToken>())).Do(
                Callback.Always(async x =>
                {
                    await _systemUnderTest.EventMessageReceived(new EventMessage
                    {
                        Event = @event.ToBytes()
                    }, CancellationToken.None);
                }));

            // Act
            _systemUnderTest.RegisterEventHandler(_eventHandler);
            await _systemUnderTest.PublishAsync(@event, CancellationToken.None);

            // Assert
            await _busService.Received().PublishAsync(Arg.Any<EventMessage>(), Arg.Any<CancellationToken>());

            await _eventHandler.Received()
                .ExecuteInternalAsync(
                    Arg.Is<EventFake>(x =>
                        x.Model.Id == ((EventFake) @event).Model.Id && x.Model.Name == ((EventFake) @event).Model.Name),
                    Arg.Any<CancellationToken>());
        }
    }
}