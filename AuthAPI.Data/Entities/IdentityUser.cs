namespace Gateway.Data.Entities;

public class IdentityUser
{
    public string RefreshToken { get; set; } = null!;
    public Guid UserId { get; set; }
    public DateTime ExpireAt { get; set; }
    public User? User { get; set; }
}