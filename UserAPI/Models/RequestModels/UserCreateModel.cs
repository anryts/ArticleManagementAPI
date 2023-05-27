using Common.Enums;

namespace UserAPI.Models.RequestModels;

public class UserCreateModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public Gender? Gender { get; set; } 
}