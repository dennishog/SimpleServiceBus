using DS.SimpleServiceBus.ConsoleApp.Commands.CommandHandlers;
using DS.SimpleServiceBus.ConsoleApp.Commands.Models.Request;
using DS.SimpleServiceBus.ConsoleApp.Commands.Models.Response;
using DS.SimpleServiceBus.ConsoleApp.Events;
using DS.SimpleServiceBus.ConsoleApp.Events.EventHandlers;
using DS.SimpleServiceBus.ConsoleApp.Events.Models;
using DS.SimpleServiceBus.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DS.SimpleServiceBus.ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            Console.WriteLine("Hello World!");

            var busService = new BusService(cfg =>
            {
                cfg.Uri = "rabbitmq://localhost/dsevents";
                cfg.Username = "guest";
                cfg.Password = "guest";
            });

            await busService.StartAsync(CancellationToken.None);

            var eventService = new EventService(busService, cfg => cfg.EventQueueName = "ds.events");
            eventService.RegisterEventHandler<TestEventListener>();
            eventService.RegisterEventHandler<TestEventListener2>();

            var commandService = new CommandService(busService, cfg => cfg.CommandQueueName = "ds.commands");
            commandService.RegisterCommandHandler<TestCommandHandler>();

            var commandService2 = new CommandService(busService, cfg => cfg.CommandQueueName = "ds.commands2");
            commandService2.RegisterCommandHandler<TestCommandHandler2>();


            await eventService.PublishAsync(new TestEvent
            {
                Model = new TestModel { Id = 10, Name = "Dennis" }
            }, CancellationToken.None);

            eventService.PublishAsync(new TestEvent2
            {
                Model = new TestModel { Id = 10, Name = "Andreas" }
            }, CancellationToken.None).Wait();

            var request = new TestRequest { Id = 10 };
            for (var i = 0; i < 20; i++)
            {
                var response =
                    await commandService.SendRequestAsync<TestRequest, TestResponse>(request, CancellationToken.None);

                Console.WriteLine($"Response with id {response.Id} and name {response.Name}");

                var response2 =
                    await commandService2.SendRequestAsync<TestRequest, TestResponse2>(request, CancellationToken.None);

                Console.WriteLine($"Response with id {response2.Id} and name {response2.Name}");
            }

            Console.ReadLine();
        }
    }
}