using Common.Enums;

namespace Gateway.Data.Entities;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Does account activated by OTP
    /// </summary>
    public bool IsActivated { get; set; }

    public string PasswordHash { get; set; } = null!;
    public Gender Gender { get; set; }
    public DateTime PasswordUpdated { get; set; }
    
    public ICollection<IdentityUser> IdentityUsers { get; set; } = new List<IdentityUser>();
    public ICollection<UserOTP> UserOtps { get; set; } = new List<UserOTP>();
}