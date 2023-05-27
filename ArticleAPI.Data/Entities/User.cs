namespace ArticleAPI.Data.Entities;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    
    /// <summary>
    /// Related on User Subscription Table.
    /// </summary>
    public bool IsSubscriptionActive { get; set; } = false;

    public ICollection<Channel> Channels { get; set; } = new List<Channel>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}