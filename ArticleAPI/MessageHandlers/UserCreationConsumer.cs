using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using Common.DTOs;
using MassTransit;

namespace ArticleAPI.MessageHandlers;

public class UserCreationConsumer : IConsumer<UserCreationDto>
{
    private readonly IUserRepository _userRepository;

    public UserCreationConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<UserCreationDto> context)
    {
        if (await _userRepository.GetByIdAsync(context.Message.UserId) is not null)
            throw new Exception("User have already created");

        await _userRepository.AddAsync(new User
        {
            Id = context.Message.UserId,
            FirstName = context.Message.FirstName,
            LastName = context.Message.LastName,
            Email = context.Message.Email,
            PhoneNumber = context.Message.PhoneNumber,
            IsSubscriptionActive = false,
        });
    }
}