using Common.Enums;

namespace UserAPI.Models.RequestModels;

public class UserSignInModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    /// <summary>
    /// 1 - ViaSms
    /// 2- ViaEmail 
    /// </summary>
    public OTPSendServiceType OtpSendServiceType { get; set; }
}