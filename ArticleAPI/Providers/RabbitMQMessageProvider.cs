using System.Text;
using ArticleAPI.Providers.Interfaces;
using Common.Enums;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ArticleAPI.Providers;

public class RabbitMqMessageProvider : IMessageProvider
{
    private readonly ConnectionFactory _connectionFactory;
    
    public RabbitMqMessageProvider()
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };
    }

    public void SendMessage<T>(T message, string exchangeName, RoutingKey routingKey)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey.ToString(), null, body: body);
        Console.WriteLine($"Message sent: {json}");
    }
}