using Common.Enums;

namespace PaymentAPI.Data.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Stripe subscription id
    /// </summary>
    public string? SubscriptionStripeId { get; set; } 

    /// <summary>
    /// Does user have subscription on stripe 
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Free - 0 or Standard - 1
    /// </summary>
    public SubscriptionType SubscriptionType { get; set; }

    /// <summary>
    /// If user has subscription on stripe but have not paid for it he can do it later
    /// </summary>
    public bool IsPastDue { get; set; }

    public User? User { get; set; }
}