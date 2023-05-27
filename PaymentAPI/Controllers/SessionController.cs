using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Data.Repositories.Interfaces;
using Stripe;
using UserAPI.API.Services.Interfaces;

namespace PaymentAPI.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class SessionController : ControllerBase
{
    private readonly IStripeClient _stripeClient;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public SessionController(IStripeClient stripeClient, IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _stripeClient = stripeClient;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var userId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(userId) ??
                   throw new Exception("User not found");
        
        var options = new Stripe.BillingPortal.SessionCreateOptions
        {
            Customer = user.CustomerId,
            ReturnUrl = "http://localhost:5004"
        };
        var service = new Stripe.BillingPortal.SessionService(_stripeClient);
        var session = await service.CreateAsync(options);
        return Ok(session.Url);
    }
}