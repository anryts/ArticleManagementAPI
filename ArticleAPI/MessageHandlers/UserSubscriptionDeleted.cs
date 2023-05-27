using System.Text.Json;
using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using Common.DTOs;
using Common.Enums;
using Common.Events;
using Common.Messages;
using Common.Options;
using Microsoft.Extensions.Options;

namespace ArticleAPI.MessageHandlers;

public class UserSubscriptionDeleted : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly BaseMessageHandler _baseMessageHandler;

    public UserSubscriptionDeleted(IServiceScopeFactory serviceScopeFactory, IOptions<RabbitMQOptions> rabbitMqOptions)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _baseMessageHandler = new BaseMessageHandler("subscription", RoutingKey.Delete, rabbitMqOptions);
        _baseMessageHandler.MessageReceived += OnMessageReceived;
    }

    private async void OnMessageReceived(object? sender, MessageReceivedEventArgs eventArgs)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var userUpdatedSubscription = JsonSerializer.Deserialize<UserCreatedSubscriptionDto>(eventArgs.Message)
                                      ?? throw new Exception("Empty message");
        var user = await userRepository.GetByIdAsync(userUpdatedSubscription.UserId);
        user.IsSubscriptionActive = userUpdatedSubscription.SubscriptionType switch
        {
            SubscriptionType.Free => false,
            SubscriptionType.Standard => true,
            _ => throw new ArgumentOutOfRangeException()
        };
        await userRepository.UpdateAsync(user);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _baseMessageHandler.ExecuteAsync();
            await Task.Delay(1000, stoppingToken);
        }

        _baseMessageHandler.Dispose();
    }
}