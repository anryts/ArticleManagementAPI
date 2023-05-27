using System.Text;
using Common.Enums;
using Common.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Common.Messages;

public class BaseMessageProvider : RabbitMqClientBase
{
    public BaseMessageProvider(IOptions<RabbitMQOptions> rabbitMqOptions) : base(rabbitMqOptions)
    { }
    
    public void SendMessage<T>(T message, string exchangeName, RoutingKey routingKey)
    {
        using var connection = ConnectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey.ToString(),null, body: body);
        Console.WriteLine($"Message sent: {json}");
    }
}