using Common.DTOs;
using Common.Enums;
using Common.EventBus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data.Repositories.Interfaces;
using Stripe;

namespace PaymentAPI.Subscriptions.Handlers;

public class CreatePaidSubscriptionCommand : IRequest<Guid>
{
    public Subscription Subscription { get; set; } = null!;
}

public class CreatePaidSubscriptionCommandHandler : IRequestHandler<CreatePaidSubscriptionCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IEventBus _eventBus;

    public CreatePaidSubscriptionCommandHandler(IUserRepository userRepository,
         IEventBus eventBus)
    {
        _userRepository = userRepository;
        _eventBus = eventBus;
    }

    async Task<Guid> IRequestHandler<CreatePaidSubscriptionCommand, Guid>.Handle(CreatePaidSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository
                       .GetByQueryAsync(q => q.Include(user => user.Subscription)
                           .FirstOrDefaultAsync(user => user.CustomerId == request.Subscription.CustomerId))
                   ?? throw new Exception("User not found");

        user.Subscription.SubscriptionType = SubscriptionType.Standard;
        user.Subscription.SubscriptionStripeId = request.Subscription.Id;
        user.Subscription.IsActive = true;

        var userSubscription = new UserCreatedSubscriptionDto
        {
            UserId = user.Id,
            SubscriptionType = user.Subscription.SubscriptionType
        };
        await _eventBus.Publish(userSubscription, cancellationToken);
        await _userRepository.UpdateAsync(user);
        return user.Subscription.Id;
    }
}