﻿using System;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.ConsoleApp.Commands.CommandHandlers;
using DS.SimpleServiceBus.ConsoleApp.Commands.Models.Request;
using DS.SimpleServiceBus.ConsoleApp.Commands.Models.Response;
using DS.SimpleServiceBus.ConsoleApp.Events;
using DS.SimpleServiceBus.ConsoleApp.Events.EventHandlers;
using DS.SimpleServiceBus.ConsoleApp.Events.Models;
using DS.SimpleServiceBus.Factories;
using DS.SimpleServiceBus.InMemory.Extensions;

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

            #region RabbitMq

            //var busService = BusServiceFactory.Create.UsingRabbitMq(cfg =>
            //{
            //    cfg.Uri = "rabbitmq://localhost/dsevents";
            //    cfg.Username = "guest";
            //    cfg.Password = "guest";
            //});

            var busService = BusServiceFactory.Create.UsingInMemory(cfg =>
            {
                cfg.QueuePath = @"C:\queues\";
            });

            await busService.StartAsync(CancellationToken.None);

            var eventService =
                EventServiceFactory.Create.UsingInMemory(busService, x => x.EventQueueName = "ds.events");
            eventService.RegisterEventHandler<TestEventListener>();
            eventService.RegisterEventHandler<TestEventListener2>();

            var commandService =
                CommandServiceFactory.Create.UsingInMemory(busService, x => x.CommandQueueName = "ds.command");
            commandService.RegisterCommandHandler<TestCommandHandler>();

            var commandService2 =
                CommandServiceFactory.Create.UsingInMemory(busService, x => x.CommandQueueName = "ds.commands2");
            commandService2.RegisterCommandHandler<TestCommandHandler2>();
            
            var @event = new TestEvent(() => new TestModel() { Id = 19, Name = "Dennis Andreas" });
            @event.Model.Name = "Arne";
            @event.Model.Id = null;
            await eventService.PublishAsync(@event, CancellationToken.None);

            var @event2 = new TestEvent2(() => new TestModel() { Id = 16, Name = "Pelle" });
            @event2.Model.Name = "Bertil";
            @event2.Model.Id = null;
            await eventService.PublishAsync(@event2, CancellationToken.None);
            
            var request = new TestRequest {Id = 10};
            for (var i = 0; i < 10; i++)
            {
                var response =
                    await commandService.SendRequestAsync<TestRequest, TestResponse>(request, CancellationToken.None);

                Console.WriteLine($"Response with id {response.Id} and name {response.Name}");

                var response2 =
                    await commandService2.SendRequestAsync<TestRequest, TestResponse2>(request, CancellationToken.None);

                Console.WriteLine($"Response with id {response2.Id} and name {response2.Name}");
            }

            Console.ReadLine();

            await commandService.StopAsync(CancellationToken.None);
            await commandService2.StopAsync(CancellationToken.None);
            await eventService.StopAsync(CancellationToken.None);

            #endregion
        }
    }
}