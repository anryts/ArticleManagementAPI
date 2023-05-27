using Common.DTOs;
using MassTransit;
using UserAPI.Data.Entities;
using UserAPI.Data.Repositories.Interfaces;

namespace UserAPI.MessageHandlers;

public class UserCreationConsumer : IConsumer<UserCreationDto>
{
    private readonly IUserRepository _userRepository;

    public UserCreationConsumer(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task Consume(ConsumeContext<UserCreationDto> context)
    {
        if (await _userRepository.GetByIdAsync(context.Message.UserId) is not null)
            throw new Exception("User have already created");

        var user = new User
        {
            Id = context.Message.UserId,
            FirstName = context.Message.FirstName,
            LastName = context.Message.LastName,
            Email = context.Message.Email,
            PhoneNumber = context.Message.PhoneNumber,
            PasswordHash = context.Message.PasswordHash,
            PasswordUpdated = context.Message.PasswordUpdated,
            IsActivated = true
        };

        await _userRepository.AddAsync(user);
    }
}