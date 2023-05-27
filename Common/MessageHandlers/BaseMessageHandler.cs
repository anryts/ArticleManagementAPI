using System.Text;
using Common.Enums;
using Common.Events;
using Common.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.MessageHandlers;

public class BaseMessageHandler : RabbitMqClientBase, IDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    public event EventHandler<MessageReceivedEventArgs>? MessageReceived;
    private readonly EventingBasicConsumer _eventingBasicConsumer;
    private readonly IModel _channel;
    private readonly string _queueName;

    public BaseMessageHandler(string exchangeName,
        RoutingKey routingKey, IOptions<RabbitMQOptions> options) : base(options)
    {
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare()
            .QueueName;
        _channel.QueueBind(
            queue: _queueName,
            exchange: exchangeName,
            routingKey: routingKey.ToString());

        _eventingBasicConsumer = new EventingBasicConsumer(_channel);
    }

    public void ExecuteAsync()
    {
        _eventingBasicConsumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs { Message = message });
        };
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: _eventingBasicConsumer);
    }
    
    public new void Dispose()
    {
       base.Dispose();
       _channel.Dispose();
    }
}