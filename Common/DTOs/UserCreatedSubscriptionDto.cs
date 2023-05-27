using Common.Enums;

namespace Common.DTOs;

public class UserCreatedSubscriptionDto
{
    public Guid UserId { get; set; }
    public SubscriptionType SubscriptionType { get; set; }
}