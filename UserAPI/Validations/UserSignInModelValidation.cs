using FluentValidation;
using UserAPI.Models.RequestModels;
using UserAPI.Validations.CustomValidations;

namespace UserAPI.Validations;

public class UserSignInModelValidation : AbstractValidator<UserSignInModel>
{
    public UserSignInModelValidation()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email is required");
        
        RuleFor(x => x.Password)
            .PasswordValidation();
        
        RuleFor(x => x.OtpSendServiceType)
            .IsInEnum()
            .WithMessage("OtpSendServiceType is required");
    }
}