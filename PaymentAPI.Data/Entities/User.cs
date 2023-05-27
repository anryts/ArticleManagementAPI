namespace PaymentAPI.Data.Entities;

public class User
{
    public Guid Id { get; set; }

    /// <summary>
    /// User id in stripe 
    /// </summary>
    public string? CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public Subscription? Subscription { get; set; }
}