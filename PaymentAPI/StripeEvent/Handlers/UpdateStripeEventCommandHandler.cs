using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data.Repositories.Interfaces;
using PaymentAPI.Subscriptions.Handlers;
using Stripe;

namespace PaymentAPI.StripeEvent.Handlers;

public class UpdateStripeEventCommand : IRequest
{
    public Subscription Subscription { get; set; } = null!;
}

public class UpdateStripeEventCommandHandler : IRequestHandler<UpdateStripeEventCommand>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IMediator _mediator;

    public UpdateStripeEventCommandHandler(IMediator mediator, ISubscriptionRepository subscriptionRepository)
    {
        _mediator = mediator;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<Unit> Handle(UpdateStripeEventCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository
                               .GetByQueryAsync(q => q
                                   .FirstOrDefaultAsync(subscription =>
                                       subscription.SubscriptionStripeId == request.Subscription.Id))
                           ?? throw new SubscriptionNotFound("Subscription not found");

        if (request.Subscription.CanceledAt is not null)
        {
            await _mediator.Send(new CancelSubscriptionCommand
            {
                SubscriptionId = request.Subscription.Id,
            });
        }

        //renew subscriptions
        if (request.Subscription.Status == SubscriptionStatuses.Active)
        {
            await _mediator.Send(new RenewSubscriptionCommand
            {
                SubscriptionId = request.Subscription.Id
            });
        }

        //handle expired subscriptions 
        if (request.Subscription.Status == SubscriptionStatuses.PastDue)
        {
            subscription.IsPastDue = true;
            await _subscriptionRepository.UpdateAsync(subscription);
        }

        throw new Exception("Subscription status is not supported");
    }
}