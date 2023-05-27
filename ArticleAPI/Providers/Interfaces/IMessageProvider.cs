using Common.Enums;

namespace ArticleAPI.Providers.Interfaces;

public interface IMessageProvider
{
    /// <summary>
    /// Publish your message to RabbitMQ
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="exchangeName">Name of exchange</param>
    /// <param name="routingKey">Type of CRUD</param>
    /// <typeparam name="T">Your message</typeparam>
    void SendMessage<T>(T message,string exchangeName, RoutingKey routingKey);
}