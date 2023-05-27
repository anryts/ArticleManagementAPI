using Common.Enums;

namespace Common.DTOs;

public class UserRenewSubscriptionDto
{
    public Guid UserId { get; set; }
    public SubscriptionType SubscriptionType { get; set; }
}