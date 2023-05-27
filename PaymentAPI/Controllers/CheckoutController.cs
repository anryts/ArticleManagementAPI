using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Checkout;

namespace PaymentAPI.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class CheckoutController : ControllerBase
{
    private readonly IMediator _mediator;

    public CheckoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create checkout session
    /// </summary>
    /// <param name="priceId"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(string priceId)
    {
        var result = await _mediator.Send(new CreateCheckoutSessionCommand
        {
            PriceId = priceId
        });
        return result;
    }
}