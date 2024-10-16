namespace Infrastructure.Common.MessageBrokers.RabbitMQ
{
    public interface IMessageBrokerOptions
    {
        string Host { get; set; }
        string Password { get; set; }
        ushort? Port { get; set; }
        string User { get; set; }
        string VirtualHost { get; set; }
    }
}