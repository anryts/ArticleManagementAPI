using Common.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Common.MessageHandlers;

public class RabbitMqClientBase : IDisposable
{
    protected IModel Channel { get; set; }
    protected IConnection _connection;
    private readonly ConnectionFactory _connectionFactory;
    private readonly IOptions<RabbitMQOptions> _options;

    protected RabbitMqClientBase(IOptions<RabbitMQOptions> options)
    {
        _options = options;
        _connectionFactory = new ConnectionFactory
        {
            HostName = _options.Value.HostName,
            Port = _options.Value.Port,
            UserName = _options.Value.UserName,
            Password = _options.Value.Password
        };
        _connection = _connectionFactory.CreateConnection();
    }

    public void Dispose()
    {
        try
        {
            Channel?.Close();
            Channel?.Dispose();
            Channel = null;

            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cannot dispose RabbitMQ channel or connection\n {ex.Message}");
        }
    }
}