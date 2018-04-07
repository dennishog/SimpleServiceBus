# SimpleServiceBus

## Install:
Install using nuget DS.SimpleServiceBus

## Usage:
Install RabbitMQ and use the management gui to create a new virtual host

Create an instance of BusService:
```C#
var busService = new BusService(cfg =>
{
	cfg.Uri = "rabbitmq://localhost/yourvirtualhostname";
    cfg.Username = "yourusername";
    cfg.Password = "yourpassword";
});
```

Start the Bus
```C#
await busService.StartAsync(CancellationToken.None);
```

## Events
Create a model class implementing the IModel interface
Create an event class implementing the IEvent<TModel> using your newly created model class (set EventId to a new guid)
Create an eventhandler implementing the IEventHandler<TEvent> using your newly created event class

Create an instance of EventService
```C#
var eventService = new EventService(busService, cfg => cfg.EventQueueName = "thiseventserviceuniquequeuename");
```

Register your eventhandler
```C#
eventService.RegisterEventHandler<YourEventHandler>();
```

Publish an event
```C#
await eventService.PublishAsync(InstanceOfYourEventClass, CancellationToken.None);
```

## Commands
Create a class implementing the IRequestModel interface
Create a class implementing the IResponseModel interface
Create a commandhandler implementing the ICommandHandler<TRequestModel, TResponseModel> using your newly created classes

Create an instance of CommandService
```C#
var commandService = new CommandService(busService, cfg => cfg.CommandQueueName = "thiscommandserviceuniquequeuename");
```

Register your commandhandler
```C#
commandService.RegisterCommandHandler<YourCommandHandler>();
```

Send a request
```C#
var response = await commandService.SendRequestAsync<YourRequestClass, YourResponseClass>(instanceOfRequestClass, CancellationToken.None);
```