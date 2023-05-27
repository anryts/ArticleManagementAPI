using ArticleAPI.Data.Repositories.Interfaces;
using Common.DTOs;
using MassTransit;

namespace ArticleAPI.MessageHandlers;

public class UserSubscriptionCreatedConsumer : IConsumer<UserCreatedSubscriptionDto>
{
    private readonly IUserRepository _userRepository;

    public UserSubscriptionCreatedConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<UserCreatedSubscriptionDto> context)
    {
        var user = await _userRepository.GetByIdAsync(context.Message.UserId) ??
            throw new Exception("User not found");

        user.IsSubscriptionActive = true;
        await _userRepository.UpdateAsync(user);
    }
}