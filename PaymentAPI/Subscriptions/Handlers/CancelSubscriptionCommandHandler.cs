using Common.DTOs;
using Common.Enums;
using Common.EventBus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data.Repositories.Interfaces;

namespace PaymentAPI.Subscriptions.Handlers;

public class CancelSubscriptionCommand : IRequest
{
    public string SubscriptionId { get; set; } = null!;
}

public class CancelSubscriptionCommandHandler : IRequestHandler<CancelSubscriptionCommand, Unit>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IEventBus _eventBus;

    public CancelSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository,
         IEventBus eventBus)
    {
        _subscriptionRepository = subscriptionRepository;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository
                               .GetByQueryAsync(q => q
                                   .Include(sub => sub.User)
                                   .FirstOrDefaultAsync(sub => sub.SubscriptionStripeId == request.SubscriptionId))
                           ?? throw new Exception("Subscription not exists");
        
        subscription.SubscriptionType = SubscriptionType.Free;
        subscription.IsActive = false;
        subscription.SubscriptionStripeId = null;
        
        var userSubscription = new UserCancelledSubscriptionDto
        {
            UserId = subscription.UserId,
        };
        await _eventBus.Publish(userSubscription, cancellationToken);
        await _subscriptionRepository.UpdateAsync(subscription);
        
        return Unit.Value;
    }
}