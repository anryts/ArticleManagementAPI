namespace Common.Models.RequestModels;

public class CreateNewPasswordModel
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}