namespace Common.Events;

public class MessageReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Message from the RabbitMQ
    /// </summary>
    public string Message { get; set; } = null!;
}