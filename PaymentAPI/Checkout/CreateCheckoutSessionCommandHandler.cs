using Common.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaymentAPI.Data.Repositories.Interfaces;
using Stripe;
using Stripe.Checkout;
using UserAPI.API.Services.Interfaces;

namespace PaymentAPI.Checkout;

public class CreateCheckoutSessionCommand : IRequest<IActionResult>
{
    public string PriceId { get; set; } = null!;
}

public class CreateCheckoutSessionCommandHandler : IRequestHandler<CreateCheckoutSessionCommand, IActionResult>
{
    private readonly IStripeClient _stripeClient;
    private readonly IUserRepository _userRepository;
    private readonly IOptions<StripeOptions> _stripeOptions;
    private readonly ICurrentUserService _currentUserService;

    public CreateCheckoutSessionCommandHandler(IStripeClient stripeClient, IUserRepository userRepository,
        IOptions<StripeOptions> stripeOptions, ICurrentUserService currentUserService)
    {
        _stripeClient = stripeClient;
        _userRepository = userRepository;
        _stripeOptions = stripeOptions;
        _currentUserService = currentUserService;
    }

    public async Task<IActionResult> Handle(CreateCheckoutSessionCommand request, CancellationToken cancellationToken)
    {
        if (request.PriceId != _stripeOptions.Value.MonthlyPriceId &&
            request.PriceId != _stripeOptions.Value.YearlyPriceId)
            return new BadRequestObjectResult("Invalid price id");
        var user = await _userRepository.GetByQueryAsync(q => q.Include(user => user.Subscription)
            .FirstOrDefaultAsync(user => user.Id == _currentUserService.GetCurrentUserId()));

        if (!string.IsNullOrWhiteSpace(user.Subscription.SubscriptionStripeId))
            return new BadRequestObjectResult("You have already subscribed to this plan");

        var options = new SessionCreateOptions
        {
            Customer = user.CustomerId,
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = request.PriceId,
                    Quantity = 1,
                }
            },
            Mode = "subscription",
            SuccessUrl = "http://localhost:5000/checkout/success?"+ "session_id={CHECKOUT_SESSION_ID}",
            CancelUrl = "http://localhost:5000/checkout/canceled?session_id={CHECKOUT_SESSION_ID}"
           
        };
        var service = new SessionService(_stripeClient);
        Session session = await service.CreateAsync(options);
        return new OkObjectResult(session.Url);
    }
}