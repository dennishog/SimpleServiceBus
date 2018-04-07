namespace DS.SimpleServiceBus.Configuration.Interfaces
{
    public interface ICommandServiceConfiguration
    {
        string CommandQueueName { get; set; }
    }
}