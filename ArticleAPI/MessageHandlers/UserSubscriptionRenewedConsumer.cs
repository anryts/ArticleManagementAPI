using ArticleAPI.Data.Repositories.Interfaces;
using Common.DTOs;
using MassTransit;

namespace ArticleAPI.MessageHandlers;

public class UserSubscriptionRenewedConsumer : IConsumer<UserRenewSubscriptionDto>
{
    private readonly IUserRepository _userRepository;
    
    public UserSubscriptionRenewedConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task Consume(ConsumeContext<UserRenewSubscriptionDto> context)
    {
        var user = await _userRepository.GetByIdAsync(context.Message.UserId)
            ?? throw new Exception("User not found");
        
        user.IsSubscriptionActive = true;
        await _userRepository.UpdateAsync(user);
    }
}