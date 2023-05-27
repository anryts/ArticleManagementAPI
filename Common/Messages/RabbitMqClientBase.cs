using Common.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Common.Messages;

public class RabbitMqClientBase : IDisposable
{
    protected IModel Channel { get; set; }
    protected IConnection Connection;
    protected readonly ConnectionFactory ConnectionFactory;
    private readonly IOptions<RabbitMQOptions> _rabbitMqOptions;

    protected RabbitMqClientBase(IOptions<RabbitMQOptions> rabbitMqOptions)
    {
        _rabbitMqOptions = rabbitMqOptions;
        ConnectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.Value.HostName,
            Port = _rabbitMqOptions.Value.Port,
            UserName = _rabbitMqOptions.Value.UserName,
            Password = _rabbitMqOptions.Value.Password
        };
        Connection = ConnectionFactory.CreateConnection();
    }

    public void Dispose()
    {
        try
        {
            Channel?.Close();
            Channel?.Dispose();
            Channel = null;

            Connection?.Close();
            Connection?.Dispose();
            Connection = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cannot dispose RabbitMQ channel or connection\n {ex.Message}");
        }
    }
}