using ArticleAPI.Data.Repositories.Interfaces;
using Common.DTOs;
using MassTransit;

namespace ArticleAPI.MessageHandlers;

public class UserSubscriptionCancelledConsumer : IConsumer<UserCancelledSubscriptionDto>
{
    private readonly IUserRepository _userRepository;
    
    public UserSubscriptionCancelledConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task Consume(ConsumeContext<UserCancelledSubscriptionDto> context)
    {
        var user = await _userRepository.GetByIdAsync(context.Message.UserId)
            ?? throw new Exception("User not found");
        user.IsSubscriptionActive = false; 
        await _userRepository.UpdateAsync(user);
    }
} 