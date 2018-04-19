# SimpleServiceBus

## RabbitMq

### Installation
Install using nuget DS.SimpleServiceBus.RabbitMq
Install RabbitMQ and use the management gui to create a new virtual host

### Usage
Create an instance of BusService:
```C#
var busService = BusServiceFactory.Create.UsingRabbitMq(cfg =>
{
    cfg.Uri = "rabbitmq://localhost/dsevents";
    cfg.Username = "guest";
    cfg.Password = "guest";
});
```

Start the Bus
```C#
await busService.StartAsync(CancellationToken.None);
```

## EventHubs

### Installation
Install using nuget DS.SimpleServiceBus.EventHubs
Go to Azure portal and create a new EventHub and a storage account.

### Usage
Create an instance of BusService:
```C#
var eventHubsBusService = BusServiceFactory.Create.UsingEventHubs(x =>
            {
    x.ConsumerGroup = "{YourConsumerGroupName}";
    x.EventHubConnectionString = "{YourEventHubCS}";
    x.EventHubName = "{TheNameOfYourEventHub}";
    x.StorageConnectionString = "{YourStorageCS}";
    x.StorageAccountName = "{YourStorageAccountName}";
});
```

Start the Bus
```C#
await busService.StartAsync(CancellationToken.None);
```

## Events
- Create a model class implementing the IModel interface
- Create an event class implementing the IEvent<TModel> using your newly created model class
- Create an eventhandler implementing the IEventHandler<TEvent> using your newly created event class

Create an instance of EventService
```C#
var eventService = EventServiceFactory.Create.UsingRabbitMq(busService, cfg => cfg.EventQueueName = "thiseventserviceuniquequeuename");
```

Register your eventhandler
```C#
eventService.RegisterEventHandler<YourEventHandler>();
```

Publish an event
```C#
await eventService.PublishAsync(InstanceOfYourEventClass, CancellationToken.None);
```

## Commands (not available for EventHubs)
- Create a class implementing the IRequestModel interface
- Create a class implementing the IResponseModel interface
- Create a commandhandler implementing the ICommandHandler<TRequestModel, TResponseModel> using your newly created classes

Create an instance of CommandService
```C#
var commandService = CommandServiceFactory.Create.UsingRabbitMq(busService, cfg => cfg.CommandQueueName = "thiscommandserviceuniquequeuename");
```

Register your commandhandler
```C#
commandService.RegisterCommandHandler<YourCommandHandler>();
```

Send a request
```C#
var response = await commandService.SendRequestAsync<YourRequestClass, YourResponseClass>(instanceOfRequestClass, CancellationToken.None);
```