namespace Common.EventBus;

public interface IEventBus
{
    public Task Publish<T>(T message, CancellationToken cts = default) where T : class;
}