namespace DS.SimpleServiceBus.Configuration.Interfaces
{
    public interface IBusServiceConfiguration
    {
        string Uri { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}