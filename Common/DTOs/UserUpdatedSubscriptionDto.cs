using Common.Enums;

namespace Common.DTOs;

public class UserUpdatedSubscriptionDto
{
    public Guid UserId { get; set; }
    public SubscriptionType SubscriptionType { get; set; }
}