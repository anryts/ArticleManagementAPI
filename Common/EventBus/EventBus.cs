using MassTransit;

namespace Common.EventBus;

public class EventBus : IEventBus
{
    private readonly IBusControl _busControl;

    public EventBus(IBusControl busControl)
    {
        _busControl = busControl;
    }

    public async Task Publish<T>(T message, CancellationToken cts = default) where T : class
    {
        await _busControl.StartAsync(cts);
        try
        {
            await _busControl.Publish(message, cts);
        }
        finally
        {
            await _busControl.StopAsync(CancellationToken.None);
        }
    }
}