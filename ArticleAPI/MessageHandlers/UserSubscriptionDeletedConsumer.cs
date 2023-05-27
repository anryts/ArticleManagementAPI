using ArticleAPI.Data.Repositories.Interfaces;
using Common.DTOs;
using MassTransit;

namespace ArticleAPI.MessageHandlers;

public class UserSubscriptionDeletedConsumer : IConsumer<UserUpdatedSubscriptionDto>
{
    private readonly IUserRepository _userRepository;
    
    public UserSubscriptionDeletedConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task Consume(ConsumeContext<UserUpdatedSubscriptionDto> context)
    {
        var user = await _userRepository.GetByIdAsync(context.Message.UserId) ??
                   throw new Exception("User haven't created");

        user.IsSubscriptionActive = false;
        await _userRepository.UpdateAsync(user);
    }
}