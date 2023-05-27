using System.Text.Json;
using AuthAPI.Data.Repositories.Interfaces;
using Common.DTOs;
using Common.Enums;
using Common.Events;
using Common.Messages;
using Common.Options;
using Gateway.Data.Entities;
using Microsoft.Extensions.Options;

namespace APIGateway.MessageHandlers;

public class UserCreationMessageHandler : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly BaseMessageHandler _baseMessageHandler;
    private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1);

    public UserCreationMessageHandler(IServiceScopeFactory serviceScopeFactory,
        IOptions<RabbitMQOptions> rabbitMqOptions)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _baseMessageHandler = new BaseMessageHandler("user", RoutingKey.Create, rabbitMqOptions);
        _baseMessageHandler.MessageReceived += OnMessageReceived;
    }

    private async void OnMessageReceived(object? sender, MessageReceivedEventArgs eventArgs)
    {
        try
        {
            await Semaphore.WaitAsync();
            using var scope = _serviceScopeFactory.CreateScope();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var userDto = JsonSerializer.Deserialize<UserCreationDto>(eventArgs.Message)
                       ?? throw new Exception("User is null");

            var result = await userRepository.GetByIdAsync(userDto.UserId);
            if (result is not null) return;
            await userRepository.AddAsync(new User
            {
                Id = userDto.UserId,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                PasswordHash = userDto.PasswordHash,
                PasswordUpdated = userDto.PasswordUpdated,
                IsActivated = false
            });

            await userRepository.SaveChangesAsync();
        }
        finally
        {
            Semaphore.Release();
        }
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
