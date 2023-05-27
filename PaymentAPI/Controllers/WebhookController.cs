using Common.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentAPI.StripeEvent.Handlers;
using PaymentAPI.Subscriptions.Handlers;
using Stripe;

namespace PaymentAPI.Controllers;

[Route("webhook")]
[ApiController]
public class WebhookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOptions<StripeOptions> _stripeOptions;

    public WebhookController(IMediator mediator, IOptions<StripeOptions> stripeOptions)
    {
        _mediator = mediator;
        _stripeOptions = stripeOptions;
    }

    [HttpPost]
    public async Task<IActionResult> Index()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            EventUtility.ParseEvent(json);
            var signatureHeader = Request.Headers["Stripe-Signature"];
            var stripeEvent = EventUtility.ConstructEvent(json,
                signatureHeader, _stripeOptions.Value.WebhookSecret);
            switch (stripeEvent.Type)
            {
                case Events.CustomerSubscriptionCreated:
                {
                    var subscription = stripeEvent.Data.Object as Subscription
                                       ?? throw new Exception("Event data object is not a subscription");
                    var result = await _mediator.Send(new CreatePaidSubscriptionCommand
                    {
                        Subscription = subscription
                    });
                }
                    break;
                case Events.CustomerSubscriptionDeleted:
                {
                    var subscription = stripeEvent.Data.Object as Subscription
                                       ?? throw new Exception("Event data object is not a subscription");

                    var request = new CancelSubscriptionCommand
                    {
                        SubscriptionId = subscription.Id
                    };
                    await _mediator.Send(request);
                }
                    break;
                case Events.CustomerSubscriptionUpdated:
                {
                    var subscription = stripeEvent.Data.Object as Subscription
                                       ?? throw new Exception("Event data object is not a subscription");
                    await _mediator.Send(new UpdateStripeEventCommand
                    {
                        Subscription = subscription
                    });
                }
                    break;
                default:
                    return Ok();
            }
        }
        catch (StripeException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }
}