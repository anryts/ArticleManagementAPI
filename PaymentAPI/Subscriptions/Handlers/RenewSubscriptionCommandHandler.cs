using Common.DTOs;
using Common.Enums;
using Common.EventBus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data.Repositories.Interfaces;

namespace PaymentAPI.Subscriptions.Handlers;

public class RenewSubscriptionCommand : IRequest<Unit>
{
    public string SubscriptionId { get; set; } = null!;
}

public class RenewSubscriptionCommandHandler : IRequestHandler<RenewSubscriptionCommand, Unit>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IEventBus _eventBus;

    public RenewSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository,
        IEventBus eventBus)
    {
        _subscriptionRepository = subscriptionRepository;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(RenewSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository
                               .GetByQueryAsync(q => q
                                   .FirstOrDefaultAsync(subscription =>
                                       subscription.SubscriptionStripeId == request.SubscriptionId))
                           ?? throw new Exception("Subscription with this Stripe Id not found");

        subscription.IsActive = true;
        subscription.SubscriptionType = SubscriptionType.Standard;
        var userSubscription = new UserRenewSubscriptionDto
        {
            UserId = subscription.UserId,
            SubscriptionType = subscription.SubscriptionType
        };
        await _eventBus.Publish(userSubscription, cancellationToken);
        await _subscriptionRepository.UpdateAsync(subscription);
        return Unit.Value;
    }
}