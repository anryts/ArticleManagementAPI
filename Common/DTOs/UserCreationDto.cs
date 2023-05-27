using Common.Enums;

namespace Common.DTOs;

public class UserCreationDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public Gender Gender { get; set; }
    public string PasswordHash { get; set; } = null!;
    public DateTime PasswordUpdated { get; set; }
}